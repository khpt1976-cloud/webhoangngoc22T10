using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Application.Services;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Controllers
{
    public class JobController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IContentManager _contentManager;

        public JobController(IApplicationService applicationService, IContentManager contentManager)
        {
            _applicationService = applicationService;
            _contentManager = contentManager;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _applicationService.GetJobPostingsAsync();
            return View(jobs);
        }

        public async Task<IActionResult> Active()
        {
            var jobs = await _applicationService.GetActiveJobPostingsAsync();
            return View("Index", jobs);
        }

        public async Task<IActionResult> Featured()
        {
            var jobs = await _applicationService.GetFeaturedJobPostingsAsync();
            return View("Index", jobs);
        }

        public async Task<IActionResult> Details(string id)
        {
            var job = await _applicationService.GetJobPostingByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        public IActionResult Create()
        {
            var viewModel = new JobPostingPartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPostingPartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobPostingPart = new JobPostingPart
                {
                    JobId = { Text = viewModel.JobId },
                    JobTitle = { Text = viewModel.JobTitle },
                    JobDescription = { Html = viewModel.JobDescription },
                    Requirements = { Html = viewModel.Requirements },
                    Benefits = { Html = viewModel.Benefits },
                    Department = { Text = viewModel.Department },
                    Location = { Text = viewModel.Location },
                    EmploymentType = { Text = viewModel.EmploymentType },
                    ExperienceLevel = { Text = viewModel.ExperienceLevel },
                    SalaryRange = { Text = viewModel.SalaryRange },
                    PostingDate = { Value = viewModel.PostingDate },
                    ApplicationDeadline = { Value = viewModel.ApplicationDeadline },
                    IsActive = { Value = viewModel.IsActive },
                    IsFeatured = { Value = viewModel.IsFeatured },
                    ContactEmail = { Text = viewModel.ContactEmail },
                    ContactPhone = { Text = viewModel.ContactPhone },
                    HiringManager = { Text = viewModel.HiringManager },
                    JobCategory = { Text = viewModel.JobCategory },
                    Priority = { Text = viewModel.Priority },
                    AdditionalInfo = { Html = viewModel.AdditionalInfo }
                };

                await _applicationService.CreateJobPostingAsync(jobPostingPart);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var job = await _applicationService.GetJobPostingByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            var jobPart = job.As<JobPostingPart>();
            var viewModel = new JobPostingPartViewModel
            {
                JobId = jobPart?.JobId.Text ?? string.Empty,
                JobTitle = jobPart?.JobTitle.Text ?? string.Empty,
                JobDescription = jobPart?.JobDescription.Html ?? string.Empty,
                Requirements = jobPart?.Requirements.Html ?? string.Empty,
                Benefits = jobPart?.Benefits.Html ?? string.Empty,
                Department = jobPart?.Department.Text ?? string.Empty,
                Location = jobPart?.Location.Text ?? string.Empty,
                EmploymentType = jobPart?.EmploymentType.Text ?? string.Empty,
                ExperienceLevel = jobPart?.ExperienceLevel.Text ?? string.Empty,
                SalaryRange = jobPart?.SalaryRange.Text ?? string.Empty,
                PostingDate = jobPart?.PostingDate.Value,
                ApplicationDeadline = jobPart?.ApplicationDeadline.Value,
                IsActive = jobPart?.IsActive.Value ?? false,
                IsFeatured = jobPart?.IsFeatured.Value ?? false,
                ContactEmail = jobPart?.ContactEmail.Text ?? string.Empty,
                ContactPhone = jobPart?.ContactPhone.Text ?? string.Empty,
                HiringManager = jobPart?.HiringManager.Text ?? string.Empty,
                JobCategory = jobPart?.JobCategory.Text ?? string.Empty,
                Priority = jobPart?.Priority.Text ?? string.Empty,
                AdditionalInfo = jobPart?.AdditionalInfo.Html ?? string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, JobPostingPartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var job = await _applicationService.GetJobPostingByIdAsync(id);
                if (job == null)
                {
                    return NotFound();
                }

                var jobPart = job.As<JobPostingPart>();
                if (jobPart != null)
                {
                    jobPart.JobId.Text = viewModel.JobId;
                    jobPart.JobTitle.Text = viewModel.JobTitle;
                    jobPart.JobDescription.Html = viewModel.JobDescription;
                    jobPart.Requirements.Html = viewModel.Requirements;
                    jobPart.Benefits.Html = viewModel.Benefits;
                    jobPart.Department.Text = viewModel.Department;
                    jobPart.Location.Text = viewModel.Location;
                    jobPart.EmploymentType.Text = viewModel.EmploymentType;
                    jobPart.ExperienceLevel.Text = viewModel.ExperienceLevel;
                    jobPart.SalaryRange.Text = viewModel.SalaryRange;
                    jobPart.PostingDate.Value = viewModel.PostingDate;
                    jobPart.ApplicationDeadline.Value = viewModel.ApplicationDeadline;
                    jobPart.IsActive.Value = viewModel.IsActive;
                    jobPart.IsFeatured.Value = viewModel.IsFeatured;
                    jobPart.ContactEmail.Text = viewModel.ContactEmail;
                    jobPart.ContactPhone.Text = viewModel.ContactPhone;
                    jobPart.HiringManager.Text = viewModel.HiringManager;
                    jobPart.JobCategory.Text = viewModel.JobCategory;
                    jobPart.Priority.Text = viewModel.Priority;
                    jobPart.AdditionalInfo.Html = viewModel.AdditionalInfo;

                    await _applicationService.UpdateJobPostingAsync(job);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _applicationService.DeleteJobPostingAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var jobs = await _applicationService.SearchJobPostingsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("Index", jobs);
        }

        public async Task<IActionResult> Applications(string jobId)
        {
            var applications = await _applicationService.GetApplicationsByJobAsync(jobId);
            ViewBag.JobId = jobId;
            return View(applications);
        }
    }
}