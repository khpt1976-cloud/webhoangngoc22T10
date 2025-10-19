using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Application.Services;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IContentManager _contentManager;

        public ApplicationController(IApplicationService applicationService, IContentManager contentManager)
        {
            _applicationService = applicationService;
            _contentManager = contentManager;
        }

        public async Task<IActionResult> Index()
        {
            var applications = await _applicationService.GetJobApplicationsAsync();
            return View(applications);
        }

        public async Task<IActionResult> Details(string id)
        {
            var application = await _applicationService.GetJobApplicationByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        public IActionResult Create()
        {
            var viewModel = new JobApplicationPartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobApplicationPartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var applicationPart = new JobApplicationPart
                {
                    ApplicationId = { Text = viewModel.ApplicationId },
                    JobTitle = { Text = viewModel.JobTitle },
                    ApplicantName = { Text = viewModel.ApplicantName },
                    ApplicantEmail = { Text = viewModel.ApplicantEmail },
                    ApplicantPhone = { Text = viewModel.ApplicantPhone },
                    CoverLetter = { Html = viewModel.CoverLetter },
                    Experience = { Text = viewModel.Experience },
                    Skills = { Text = viewModel.Skills },
                    Education = { Text = viewModel.Education },
                    ExpectedSalary = { Text = viewModel.ExpectedSalary },
                    ApplicationStatus = { Text = viewModel.ApplicationStatus },
                    ApplicationDate = { Value = viewModel.ApplicationDate },
                    Department = { Text = viewModel.Department },
                    Position = { Text = viewModel.Position }
                };

                await _applicationService.CreateJobApplicationAsync(applicationPart);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var application = await _applicationService.GetJobApplicationByIdAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            var applicationPart = application.As<JobApplicationPart>();
            var viewModel = new JobApplicationPartViewModel
            {
                ApplicationId = applicationPart?.ApplicationId.Text ?? string.Empty,
                JobTitle = applicationPart?.JobTitle.Text ?? string.Empty,
                ApplicantName = applicationPart?.ApplicantName.Text ?? string.Empty,
                ApplicantEmail = applicationPart?.ApplicantEmail.Text ?? string.Empty,
                ApplicantPhone = applicationPart?.ApplicantPhone.Text ?? string.Empty,
                CoverLetter = applicationPart?.CoverLetter.Html ?? string.Empty,
                Experience = applicationPart?.Experience.Text ?? string.Empty,
                Skills = applicationPart?.Skills.Text ?? string.Empty,
                Education = applicationPart?.Education.Text ?? string.Empty,
                ExpectedSalary = applicationPart?.ExpectedSalary.Text ?? string.Empty,
                ApplicationStatus = applicationPart?.ApplicationStatus.Text ?? string.Empty,
                ApplicationDate = applicationPart?.ApplicationDate.Value,
                Department = applicationPart?.Department.Text ?? string.Empty,
                Position = applicationPart?.Position.Text ?? string.Empty
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, JobApplicationPartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var application = await _applicationService.GetJobApplicationByIdAsync(id);
                if (application == null)
                {
                    return NotFound();
                }

                var applicationPart = application.As<JobApplicationPart>();
                if (applicationPart != null)
                {
                    applicationPart.ApplicationId.Text = viewModel.ApplicationId;
                    applicationPart.JobTitle.Text = viewModel.JobTitle;
                    applicationPart.ApplicantName.Text = viewModel.ApplicantName;
                    applicationPart.ApplicantEmail.Text = viewModel.ApplicantEmail;
                    applicationPart.ApplicantPhone.Text = viewModel.ApplicantPhone;
                    applicationPart.CoverLetter.Html = viewModel.CoverLetter;
                    applicationPart.Experience.Text = viewModel.Experience;
                    applicationPart.Skills.Text = viewModel.Skills;
                    applicationPart.Education.Text = viewModel.Education;
                    applicationPart.ExpectedSalary.Text = viewModel.ExpectedSalary;
                    applicationPart.ApplicationStatus.Text = viewModel.ApplicationStatus;
                    applicationPart.ApplicationDate.Value = viewModel.ApplicationDate;
                    applicationPart.Department.Text = viewModel.Department;
                    applicationPart.Position.Text = viewModel.Position;

                    await _applicationService.UpdateJobApplicationAsync(application);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _applicationService.DeleteJobApplicationAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var applications = await _applicationService.SearchJobApplicationsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("Index", applications);
        }

        public async Task<IActionResult> ByStatus(string status)
        {
            var applications = await _applicationService.GetApplicationsByStatusAsync(status);
            ViewBag.Status = status;
            return View("Index", applications);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string id, string status)
        {
            await _applicationService.UpdateApplicationStatusAsync(id, status);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}