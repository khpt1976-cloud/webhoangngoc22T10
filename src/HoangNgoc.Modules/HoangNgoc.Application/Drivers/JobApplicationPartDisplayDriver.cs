using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Drivers
{
    public class JobApplicationPartDisplayDriver : ContentPartDisplayDriver<JobApplicationPart>
    {
        public override IDisplayResult Display(JobApplicationPart part, BuildPartDisplayContext context)
        {
            return Initialize<JobApplicationPartViewModel>(GetDisplayShapeType(context), viewModel =>
            {
                viewModel.JobApplicationPart = part;
                viewModel.ContentItem = part.ContentItem;
            })
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10");
        }

        public override IDisplayResult Edit(JobApplicationPart part, BuildPartEditorContext context)
        {
            return Initialize<JobApplicationPartViewModel>(GetEditorShapeType(context), viewModel =>
            {
                viewModel.JobApplicationPart = part;
                viewModel.ContentItem = part.ContentItem;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(JobApplicationPart part, UpdatePartEditorContext context)
        {
            var viewModel = new JobApplicationPartViewModel();
            
            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.ApplicationId.Text = viewModel.ApplicationId;
                part.JobTitle.Text = viewModel.JobTitle;
                part.ApplicantName.Text = viewModel.ApplicantName;
                part.ApplicantEmail.Text = viewModel.ApplicantEmail;
                part.ApplicantPhone.Text = viewModel.ApplicantPhone;
                part.CoverLetter.Html = viewModel.CoverLetter;
                part.Experience.Text = viewModel.Experience;
                part.Skills.Text = viewModel.Skills;
                part.Education.Text = viewModel.Education;
                part.ExpectedSalary.Text = viewModel.ExpectedSalary;
                part.ApplicationStatus.Text = viewModel.ApplicationStatus;
                part.ApplicationDate.Value = viewModel.ApplicationDate;
                part.InterviewDate.Value = viewModel.InterviewDate;
                part.InterviewNotes.Html = viewModel.InterviewNotes;
                part.InterviewResult.Text = viewModel.InterviewResult;
                part.IsShortlisted.Value = viewModel.IsShortlisted;
                part.IsHired.Value = viewModel.IsHired;
                part.HRNotes.Text = viewModel.HRNotes;
                part.Department.Text = viewModel.Department;
                part.Position.Text = viewModel.Position;
                part.ReferenceContact.Text = viewModel.ReferenceContact;
            }

            return Edit(part, context);
        }
    }
}