using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.ViewModels;

namespace HoangNgoc.Application.Drivers
{
    public class JobPostingPartDisplayDriver : ContentPartDisplayDriver<JobPostingPart>
    {
        public override IDisplayResult Display(JobPostingPart part, BuildPartDisplayContext context)
        {
            return Initialize<JobPostingPartViewModel>(GetDisplayShapeType(context), viewModel =>
            {
                viewModel.JobPostingPart = part;
                viewModel.ContentItem = part.ContentItem;
            })
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10");
        }

        public override IDisplayResult Edit(JobPostingPart part, BuildPartEditorContext context)
        {
            return Initialize<JobPostingPartViewModel>(GetEditorShapeType(context), viewModel =>
            {
                viewModel.JobPostingPart = part;
                viewModel.ContentItem = part.ContentItem;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(JobPostingPart part, UpdatePartEditorContext context)
        {
            var viewModel = new JobPostingPartViewModel();
            
            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.JobId.Text = viewModel.JobId;
                part.JobTitle.Text = viewModel.JobTitle;
                part.JobDescription.Html = viewModel.JobDescription;
                part.Requirements.Html = viewModel.Requirements;
                part.Benefits.Html = viewModel.Benefits;
                part.Department.Text = viewModel.Department;
                part.Location.Text = viewModel.Location;
                part.EmploymentType.Text = viewModel.EmploymentType;
                part.ExperienceLevel.Text = viewModel.ExperienceLevel;
                part.SalaryRange.Text = viewModel.SalaryRange;
                part.PostingDate.Value = viewModel.PostingDate;
                part.ApplicationDeadline.Value = viewModel.ApplicationDeadline;
                part.IsActive.Value = viewModel.IsActive;
                part.IsFeatured.Value = viewModel.IsFeatured;
                part.ApplicationCount.Value = viewModel.ApplicationCount;
                part.ContactEmail.Text = viewModel.ContactEmail;
                part.ContactPhone.Text = viewModel.ContactPhone;
                part.HiringManager.Text = viewModel.HiringManager;
                part.JobCategory.Text = viewModel.JobCategory;
                part.Priority.Text = viewModel.Priority;
                part.AdditionalInfo.Html = viewModel.AdditionalInfo;
            }

            return Edit(part, context);
        }
    }
}