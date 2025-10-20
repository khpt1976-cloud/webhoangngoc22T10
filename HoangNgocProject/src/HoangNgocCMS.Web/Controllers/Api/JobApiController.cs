using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using HoangNgoc.JobPosting.Services;
using HoangNgoc.JobPosting.ViewModels;

namespace HoangNgocCMS.Web.Controllers.Api
{
    [ApiController]
    [Route("api/jobs")]
    public class JobApiController : ControllerBase
    {
        private readonly IContentManager _contentManager;
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IUserJobService _userJobService;

        public JobApiController(
            IContentManager contentManager,
            IJobApplicationService jobApplicationService,
            IUserJobService userJobService)
        {
            _contentManager = contentManager;
            _jobApplicationService = jobApplicationService;
            _userJobService = userJobService;
        }

        // POST: /api/jobs/{id}/save
        [HttpPost("{id}/save")]
        [Authorize]
        public async Task<IActionResult> SaveJob(string id)
        {
            try
            {
                var job = await _contentManager.GetAsync(id);
                if (job == null || !job.Published || job.ContentType != "JobPosting")
                {
                    return NotFound(new { success = false, message = "Job not found" });
                }

                var userId = User.Identity.Name;
                var isSaved = await _userJobService.IsJobSavedAsync(userId, id);

                if (isSaved)
                {
                    await _userJobService.UnsaveJobAsync(userId, id);
                    return Ok(new { success = true, saved = false, message = "Job removed from saved list" });
                }
                else
                {
                    await _userJobService.SaveJobAsync(userId, id);
                    return Ok(new { success = true, saved = true, message = "Job saved successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while saving the job" });
            }
        }

        // POST: /api/jobs/{id}/view
        [HttpPost("{id}/view")]
        public async Task<IActionResult> TrackJobView(string id)
        {
            try
            {
                var job = await _contentManager.GetAsync(id);
                if (job == null || !job.Published || job.ContentType != "JobPosting")
                {
                    return NotFound(new { success = false, message = "Job not found" });
                }

                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                await _jobApplicationService.TrackJobViewAsync(id, ipAddress);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while tracking the view" });
            }
        }

        // GET: /api/jobs/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchJobs([FromQuery] JobSearchApiModel model)
        {
            try
            {
                var results = await _jobApplicationService.SearchJobsAsync(new JobSearchCriteria
                {
                    Query = model.Q,
                    Location = model.Location,
                    JobTypes = model.Type?.Split(','),
                    ExperienceLevels = model.Experience?.Split(','),
                    SalaryRanges = model.Salary?.Split(','),
                    Category = model.Category,
                    Page = model.Page ?? 1,
                    PageSize = model.PageSize ?? 12
                });

                return Ok(new
                {
                    success = true,
                    data = results.Jobs.Select(job => new
                    {
                        id = job.ContentItemId,
                        title = job.DisplayText,
                        company = job.Content.JobPosting.Company?.Text,
                        location = job.Content.JobPosting.Location?.Text,
                        salary = job.Content.JobPosting.Salary?.Text,
                        jobType = job.Content.JobPosting.JobType?.Text,
                        publishedDate = job.PublishedUtc,
                        summary = job.Content.JobPosting.Summary?.Text
                    }),
                    pagination = new
                    {
                        page = results.Page,
                        pageSize = results.PageSize,
                        totalCount = results.TotalCount,
                        totalPages = results.TotalPages,
                        hasMore = results.HasMore
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while searching jobs" });
            }
        }
    }

    // API Models
    public class JobSearchApiModel
    {
        public string Q { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public string Category { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}