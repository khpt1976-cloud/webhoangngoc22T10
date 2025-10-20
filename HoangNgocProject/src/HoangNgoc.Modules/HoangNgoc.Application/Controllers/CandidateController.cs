using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Application.Services;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IContentManager _contentManager;

        public CandidateController(IApplicationService applicationService, IContentManager contentManager)
        {
            _applicationService = applicationService;
            _contentManager = contentManager;
        }

        public async Task<IActionResult> Index()
        {
            var candidates = await _applicationService.GetCandidatesAsync();
            return View(candidates);
        }

        public async Task<IActionResult> Available()
        {
            var candidates = await _applicationService.GetAvailableCandidatesAsync();
            return View("Index", candidates);
        }

        public async Task<IActionResult> Details(string id)
        {
            var candidate = await _applicationService.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        public IActionResult Create()
        {
            var viewModel = new CandidatePartViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CandidatePartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var candidatePart = new CandidatePart
                {
                    CandidateId = { Text = viewModel.CandidateId },
                    FullName = { Text = viewModel.FullName },
                    Email = { Text = viewModel.Email },
                    Phone = { Text = viewModel.Phone },
                    DateOfBirth = { Value = viewModel.DateOfBirth },
                    Address = { Text = viewModel.Address },
                    City = { Text = viewModel.City },
                    Country = { Text = viewModel.Country },
                    Nationality = { Text = viewModel.Nationality },
                    Gender = { Text = viewModel.Gender },
                    MaritalStatus = { Text = viewModel.MaritalStatus },
                    CurrentPosition = { Text = viewModel.CurrentPosition },
                    CurrentCompany = { Text = viewModel.CurrentCompany },
                    TotalExperience = { Text = viewModel.TotalExperience },
                    Skills = { Text = viewModel.Skills },
                    Education = { Text = viewModel.Education },
                    Certifications = { Text = viewModel.Certifications },
                    Languages = { Text = viewModel.Languages },
                    ExpectedSalary = { Text = viewModel.ExpectedSalary },
                    NoticePeriod = { Text = viewModel.NoticePeriod },
                    PreferredLocation = { Text = viewModel.PreferredLocation },
                    IsAvailable = { Value = viewModel.IsAvailable },
                    CandidateSource = { Text = viewModel.CandidateSource },
                    Notes = { Html = viewModel.Notes },
                    RegistrationDate = { Value = viewModel.RegistrationDate },
                    LastUpdated = { Value = viewModel.LastUpdated }
                };

                await _applicationService.CreateCandidateAsync(candidatePart);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var candidate = await _applicationService.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            var candidatePart = candidate.As<CandidatePart>();
            var viewModel = new CandidatePartViewModel
            {
                CandidateId = candidatePart?.CandidateId.Text ?? string.Empty,
                FullName = candidatePart?.FullName.Text ?? string.Empty,
                Email = candidatePart?.Email.Text ?? string.Empty,
                Phone = candidatePart?.Phone.Text ?? string.Empty,
                DateOfBirth = candidatePart?.DateOfBirth.Value,
                Address = candidatePart?.Address.Text ?? string.Empty,
                City = candidatePart?.City.Text ?? string.Empty,
                Country = candidatePart?.Country.Text ?? string.Empty,
                Nationality = candidatePart?.Nationality.Text ?? string.Empty,
                Gender = candidatePart?.Gender.Text ?? string.Empty,
                MaritalStatus = candidatePart?.MaritalStatus.Text ?? string.Empty,
                CurrentPosition = candidatePart?.CurrentPosition.Text ?? string.Empty,
                CurrentCompany = candidatePart?.CurrentCompany.Text ?? string.Empty,
                TotalExperience = candidatePart?.TotalExperience.Text ?? string.Empty,
                Skills = candidatePart?.Skills.Text ?? string.Empty,
                Education = candidatePart?.Education.Text ?? string.Empty,
                Certifications = candidatePart?.Certifications.Text ?? string.Empty,
                Languages = candidatePart?.Languages.Text ?? string.Empty,
                ExpectedSalary = candidatePart?.ExpectedSalary.Text ?? string.Empty,
                NoticePeriod = candidatePart?.NoticePeriod.Text ?? string.Empty,
                PreferredLocation = candidatePart?.PreferredLocation.Text ?? string.Empty,
                IsAvailable = candidatePart?.IsAvailable.Value ?? false,
                CandidateSource = candidatePart?.CandidateSource.Text ?? string.Empty,
                Notes = candidatePart?.Notes.Html ?? string.Empty,
                RegistrationDate = candidatePart?.RegistrationDate.Value,
                LastUpdated = candidatePart?.LastUpdated.Value
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CandidatePartViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var candidate = await _applicationService.GetCandidateByIdAsync(id);
                if (candidate == null)
                {
                    return NotFound();
                }

                var candidatePart = candidate.As<CandidatePart>();
                if (candidatePart != null)
                {
                    candidatePart.CandidateId.Text = viewModel.CandidateId;
                    candidatePart.FullName.Text = viewModel.FullName;
                    candidatePart.Email.Text = viewModel.Email;
                    candidatePart.Phone.Text = viewModel.Phone;
                    candidatePart.DateOfBirth.Value = viewModel.DateOfBirth;
                    candidatePart.Address.Text = viewModel.Address;
                    candidatePart.City.Text = viewModel.City;
                    candidatePart.Country.Text = viewModel.Country;
                    candidatePart.Nationality.Text = viewModel.Nationality;
                    candidatePart.Gender.Text = viewModel.Gender;
                    candidatePart.MaritalStatus.Text = viewModel.MaritalStatus;
                    candidatePart.CurrentPosition.Text = viewModel.CurrentPosition;
                    candidatePart.CurrentCompany.Text = viewModel.CurrentCompany;
                    candidatePart.TotalExperience.Text = viewModel.TotalExperience;
                    candidatePart.Skills.Text = viewModel.Skills;
                    candidatePart.Education.Text = viewModel.Education;
                    candidatePart.Certifications.Text = viewModel.Certifications;
                    candidatePart.Languages.Text = viewModel.Languages;
                    candidatePart.ExpectedSalary.Text = viewModel.ExpectedSalary;
                    candidatePart.NoticePeriod.Text = viewModel.NoticePeriod;
                    candidatePart.PreferredLocation.Text = viewModel.PreferredLocation;
                    candidatePart.IsAvailable.Value = viewModel.IsAvailable;
                    candidatePart.CandidateSource.Text = viewModel.CandidateSource;
                    candidatePart.Notes.Html = viewModel.Notes;
                    candidatePart.RegistrationDate.Value = viewModel.RegistrationDate;
                    candidatePart.LastUpdated.Value = DateTime.Now;

                    await _applicationService.UpdateCandidateAsync(candidate);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _applicationService.DeleteCandidateAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var candidates = await _applicationService.SearchCandidatesAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View("Index", candidates);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsHired(string candidateId, string jobId)
        {
            await _applicationService.MarkCandidateAsHiredAsync(candidateId, jobId);
            return RedirectToAction(nameof(Details), new { id = candidateId });
        }
    }
}