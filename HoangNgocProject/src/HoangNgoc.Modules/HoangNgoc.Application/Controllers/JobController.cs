using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Admin;
using YesSql;

namespace HoangNgoc.Application.Controllers
{
    public class JobController : Controller, IUpdateModel
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;

        public JobController(
            ISession session,
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager)
        {
            _session = session;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
        }

        // Public job listing for website visitors
        public async Task<IActionResult> Index()
        {
            var jobs = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && x.Published)
                .OrderByDescending(x => x.CreatedUtc)
                .ListAsync();

            return View(jobs);
        }

        // Public job details for website visitors
        public async Task<IActionResult> Details(string id)
        {
            var job = await _contentManager.GetAsync(id);
            if (job == null || !job.Published)
            {
                return NotFound();
            }
            
            // Use display manager for proper rendering
            var shape = await _contentItemDisplayManager.BuildDisplayAsync(job, this, "Detail");
            return View(shape);
        }

        // Public search functionality
        public async Task<IActionResult> Search(string query)
        {
            var jobs = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && x.Published)
                .OrderByDescending(x => x.CreatedUtc)
                .ListAsync();

            if (!string.IsNullOrEmpty(query))
            {
                jobs = jobs.Where(j => j.DisplayText.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewData["Query"] = query;
            return View("Index", jobs);
        }
    }
}