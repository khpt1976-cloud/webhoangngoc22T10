using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Drivers
{
    public class CandidatePartDisplayDriver : ContentPartDisplayDriver<CandidatePart>
    {
        public override IDisplayResult Display(CandidatePart part, BuildPartDisplayContext context)
        {
            return Initialize<CandidatePartViewModel>(GetDisplayShapeType(context), viewModel =>
            {
                viewModel.CandidatePart = part;
                viewModel.ContentItem = part.ContentItem;
            })
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10");
        }

        public override IDisplayResult Edit(CandidatePart part, BuildPartEditorContext context)
        {
            return Initialize<CandidatePartViewModel>(GetEditorShapeType(context), viewModel =>
            {
                viewModel.CandidatePart = part;
                viewModel.ContentItem = part.ContentItem;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(CandidatePart part, UpdatePartEditorContext context)
        {
            var viewModel = new CandidatePartViewModel();
            
            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.CandidateId.Text = viewModel.CandidateId;
                part.FullName.Text = viewModel.FullName;
                part.Email.Text = viewModel.Email;
                part.Phone.Text = viewModel.Phone;
                part.DateOfBirth.Value = viewModel.DateOfBirth;
                part.Address.Text = viewModel.Address;
                part.City.Text = viewModel.City;
                part.Country.Text = viewModel.Country;
                part.Nationality.Text = viewModel.Nationality;
                part.Gender.Text = viewModel.Gender;
                part.MaritalStatus.Text = viewModel.MaritalStatus;
                part.CurrentPosition.Text = viewModel.CurrentPosition;
                part.CurrentCompany.Text = viewModel.CurrentCompany;
                part.TotalExperience.Text = viewModel.TotalExperience;
                part.Skills.Text = viewModel.Skills;
                part.Education.Text = viewModel.Education;
                part.Certifications.Text = viewModel.Certifications;
                part.Languages.Text = viewModel.Languages;
                part.ExpectedSalary.Text = viewModel.ExpectedSalary;
                part.NoticePeriod.Text = viewModel.NoticePeriod;
                part.PreferredLocation.Text = viewModel.PreferredLocation;
                part.IsAvailable.Value = viewModel.IsAvailable;
                part.CandidateSource.Text = viewModel.CandidateSource;
                part.Notes.Html = viewModel.Notes;
                part.RegistrationDate.Value = viewModel.RegistrationDate;
                part.LastUpdated.Value = viewModel.LastUpdated;
            }

            return Edit(part, context);
        }
    }
}