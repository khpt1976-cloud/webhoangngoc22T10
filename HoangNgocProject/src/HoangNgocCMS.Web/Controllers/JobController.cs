using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Settings;
using YesSql;
using HoangNgoc.JobPosting.Models;
using HoangNgoc.JobPosting.Services;
using HoangNgoc.JobPosting.ViewModels;

namespace HoangNgocCMS.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly ISession _session;
        private readonly ISiteService _siteService;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IShapeFactory _shapeFactory;

        public JobController(
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            ISession session,
            ISiteService siteService,
            IUpdateModelAccessor updateModelAccessor,
            IJobApplicationService jobApplicationService,
            IShapeFactory shapeFactory)
        {
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _session = session;
            _siteService = siteService;
            _updateModelAccessor = updateModelAccessor;
            _jobApplicationService = jobApplicationService;
            _shapeFactory = shapeFactory;
        }

        // GET: /jobs
        [HttpGet]
        public async Task<IActionResult> Index(JobListViewModel model)
        {
            var query = _session.Query<ContentItem>()
                .Where(x => x.ContentType == "JobPosting" && x.Published);

            // Apply search filter
            if (!string.IsNullOrEmpty(model.SearchQuery))
            {
                query = query.Where(x => x.DisplayText.Contains(model.SearchQuery) ||
                                        x.Content.JobPosting.Description.Text.Contains(model.SearchQuery));
            }

            // Apply location filter
            if (!string.IsNullOrEmpty(model.Location))
            {
                query = query.Where(x => x.Content.JobPosting.Location.Text.Contains(model.Location));
            }

            // Apply job type filter
            if (model.JobTypes != null && model.JobTypes.Any())
            {
                query = query.Where(x => model.JobTypes.Contains(x.Content.JobPosting.JobType.Text));
            }

            // Apply experience level filter
            if (model.ExperienceLevels != null && model.ExperienceLevels.Any())
            {
                query = query.Where(x => model.ExperienceLevels.Contains(x.Content.JobPosting.ExperienceLevel.Text));
            }

            // Apply category filter
            if (!string.IsNullOrEmpty(model.Category))
            {
                query = query.Where(x => x.Content.JobPosting.Category.Text == model.Category);
            }

            // Apply sorting
            switch (model.SortBy?.ToLower())
            {
                case "oldest":
                    query = query.OrderBy(x => x.CreatedUtc);
                    break;
                case "salary-high":
                    query = query.OrderByDescending(x => x.Content.JobPosting.SalaryMax.Value);
                    break;
                case "salary-low":
                    query = query.OrderBy(x => x.Content.JobPosting.SalaryMin.Value);
                    break;
                case "company":
                    query = query.OrderBy(x => x.Content.JobPosting.Company.Text);
                    break;
                default: // newest
                    query = query.OrderByDescending(x => x.CreatedUtc);
                    break;
            }

            // Pagination
            var pageSize = 12;
            var totalCount = await query.CountAsync();
            var jobs = await query.Skip((model.Page - 1) * pageSize).Take(pageSize).ListAsync();

            // Build display shapes
            var jobShapes = new List<dynamic>();
            foreach (var job in jobs)
            {
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(job, _updateModelAccessor.ModelUpdater, "Summary");
                jobShapes.Add(shape);
            }

            // Create view model
            var viewModel = new JobListViewModel
            {
                Jobs = jobShapes,
                SearchQuery = model.SearchQuery,
                Location = model.Location,
                JobTypes = model.JobTypes,
                ExperienceLevels = model.ExperienceLevels,
                Category = model.Category,
                SortBy = model.SortBy,
                Page = model.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                HasMoreJobs = (model.Page * pageSize) < totalCount
            };

            return View(viewModel);
        }

        // GET: /jobs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var job = await _contentManager.GetAsync(id);
            
            if (job == null || !job.Published || job.ContentType != "JobPosting")
            {
                return NotFound();
            }

            // Track job view
            await _jobApplicationService.TrackJobViewAsync(id, HttpContext.Connection.RemoteIpAddress?.ToString());

            // Build display shape
            var shape = await _contentItemDisplayManager.BuildDisplayAsync(job, _updateModelAccessor.ModelUpdater);

            // Get related jobs
            var relatedJobs = await GetRelatedJobsAsync(job);

            var viewModel = new JobDetailsViewModel
            {
                Job = shape,
                RelatedJobs = relatedJobs,
                IsUserLoggedIn = User.Identity.IsAuthenticated,
                HasUserApplied = User.Identity.IsAuthenticated ? 
                    await _jobApplicationService.HasUserAppliedAsync(id, User.Identity.Name) : false
            };

            return View(viewModel);
        }

        // GET: /jobs/apply/{id}
        [HttpGet("apply/{id}")]
        [Authorize]
        public async Task<IActionResult> Apply(string id)
        {
            var job = await _contentManager.GetAsync(id);
            
            if (job == null || !job.Published || job.ContentType != "JobPosting")
            {
                return NotFound();
            }

            // Check if user already applied
            if (await _jobApplicationService.HasUserAppliedAsync(id, User.Identity.Name))
            {
                TempData["Error"] = "You have already applied for this job.";
                return RedirectToAction("Details", new { id });
            }

            // Check application deadline
            var deadline = job.Content.JobPosting.ApplicationDeadline?.Value;
            if (deadline.HasValue && deadline.Value < DateTime.UtcNow)
            {
                TempData["Error"] = "The application deadline for this job has passed.";
                return RedirectToAction("Details", new { id });
            }

            var viewModel = new JobApplicationViewModel
            {
                JobId = id,
                JobTitle = job.DisplayText,
                CompanyName = job.Content.JobPosting.Company?.Text,
                // Pre-fill user data if available
                FirstName = User.FindFirst("given_name")?.Value,
                LastName = User.FindFirst("family_name")?.Value,
                Email = User.FindFirst("email")?.Value
            };

            return View(viewModel);
        }

        // POST: /jobs/apply
        [HttpPost("apply")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(JobApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var job = await _contentManager.GetAsync(model.JobId);
            if (job == null || !job.Published)
            {
                return NotFound();
            }

            // Check if user already applied
            if (await _jobApplicationService.HasUserAppliedAsync(model.JobId, User.Identity.Name))
            {
                ModelState.AddModelError("", "You have already applied for this job.");
                return View(model);
            }

            try
            {
                // Create job application
                var applicationId = await _jobApplicationService.CreateApplicationAsync(new JobApplicationCreateModel
                {
                    JobId = model.JobId,
                    UserId = User.Identity.Name,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    City = model.City,
                    Country = model.Country,
                    CurrentPosition = model.CurrentPosition,
                    CurrentCompany = model.CurrentCompany,
                    ExperienceYears = model.ExperienceYears,
                    ExpectedSalary = model.ExpectedSalary,
                    Education = model.Education,
                    Skills = model.Skills,
                    WorkExperience = model.WorkExperience,
                    CoverLetter = model.CoverLetter,
                    AdditionalInfo = model.AdditionalInfo,
                    CVFile = model.CVFile,
                    PortfolioFile = model.PortfolioFile,
                    AgreeTerms = model.AgreeTerms,
                    AllowContact = model.AllowContact
                });

                TempData["Success"] = "Your application has been submitted successfully!";
                return RedirectToAction("ApplicationSuccess", new { id = applicationId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while submitting your application. Please try again.");
                // Log the exception
                return View(model);
            }
        }

        // GET: /jobs/apply/success/{id}
        [HttpGet("apply/success/{id}")]
        [Authorize]
        public async Task<IActionResult> ApplicationSuccess(string id)
        {
            var application = await _jobApplicationService.GetApplicationAsync(id);
            
            if (application == null || application.UserId != User.Identity.Name)
            {
                return NotFound();
            }

            var job = await _contentManager.GetAsync(application.JobId);
            
            var viewModel = new ApplicationSuccessViewModel
            {
                ApplicationId = id,
                JobTitle = job?.DisplayText,
                CompanyName = job?.Content.JobPosting.Company?.Text,
                SubmittedDate = application.CreatedDate
            };

            return View(viewModel);
        }

        // Helper method to get related jobs
        private async Task<List<dynamic>> GetRelatedJobsAsync(ContentItem job)
        {
            var category = job.Content.JobPosting.Category?.Text;
            var location = job.Content.JobPosting.Location?.Text;

            var query = _session.Query<ContentItem>()
                .Where(x => x.ContentType == "JobPosting" && 
                           x.Published && 
                           x.ContentItemId != job.ContentItemId);

            // Prefer same category or location
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Content.JobPosting.Category.Text == category);
            }
            else if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.Content.JobPosting.Location.Text == location);
            }

            var relatedJobs = await query.Take(3).ListAsync();
            var shapes = new List<dynamic>();

            foreach (var relatedJob in relatedJobs)
            {
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(relatedJob, _updateModelAccessor.ModelUpdater, "Summary");
                shapes.Add(shape);
            }

            return shapes;
        }
    }
}