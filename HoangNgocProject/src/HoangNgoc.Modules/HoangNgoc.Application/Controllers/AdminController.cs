using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using HoangNgoc.Application.ViewModels;
using OrchardCore.Admin;
using YesSql;
using OrchardCore.Title.Models;
using OrchardCore.Html.Models;

namespace HoangNgoc.Application.Controllers
{
    [Admin("HoangNgoc.Application/{action}/{id?}", "HoangNgoc.Application{action}")]
    public class AdminController : Controller, IUpdateModel
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;

        public AdminController(
            ISession session,
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager)
        {
            _session = session;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
        }

        public async Task<IActionResult> Index()
        {
            // Load actual JobPosting ContentItems from OrchardCore
            var jobPostings = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && x.Latest)
                .OrderByDescending(x => x.ModifiedUtc)
                .ListAsync();

            // DEBUG: Create a test JobPosting if none exist
            if (!jobPostings.Any())
            {
                var testJobPosting = await _contentManager.NewAsync("JobPosting");
                testJobPosting.DisplayText = "Test Job - Senior Developer";
                testJobPosting.Alter<TitlePart>(part => part.Title = "Test Job - Senior Developer");
                testJobPosting.Alter<HtmlBodyPart>(part => part.Html = "<p>This is a test job posting created programmatically.</p>");
                
                await _contentManager.CreateAsync(testJobPosting, VersionOptions.Published);
                
                // Reload after creating test data
                jobPostings = await _session.Query<ContentItem, ContentItemIndex>()
                    .Where(x => x.ContentType == "JobPosting" && x.Latest)
                    .OrderByDescending(x => x.ModifiedUtc)
                    .ListAsync();
            }

            // Create ViewModel following OrchardCore pattern
            var model = new AdminIndexViewModel
            {
                JobPostings = jobPostings.ToList(),
                Options = new JobPostingIndexOptions()
            };
            
            return View(model);
        }

        public async Task<IActionResult> Active()
        {
            var jobs = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && x.Published)
                .OrderByDescending(x => x.ModifiedUtc)
                .ListAsync();
            
            return View("Index", jobs);
        }

        public async Task<IActionResult> Draft()
        {
            var jobs = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && !x.Published)
                .OrderByDescending(x => x.ModifiedUtc)
                .ListAsync();
            
            return View("Index", jobs);
        }

        public async Task<IActionResult> Details(string id)
        {
            var job = await _contentManager.GetAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }
    }
}