using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using HoangNgoc.Comment.Models;
using HoangNgoc.Comment.ViewModels;

namespace HoangNgoc.Comment.Drivers
{
    public class CommentPartDisplayDriver : ContentPartDisplayDriver<CommentPart>
    {
        public override IDisplayResult Display(CommentPart part, BuildPartDisplayContext context)
        {
            return Initialize<CommentPartViewModel>(GetDisplayShapeType(context), viewModel =>
            {
                viewModel.CommentId = part.CommentId.Text;
                viewModel.AuthorName = part.AuthorName.Text;
                viewModel.AuthorEmail = part.AuthorEmail.Text;
                viewModel.AuthorWebsite = part.AuthorWebsite.Text;
                viewModel.CommentContent = part.CommentContent.Html;
                viewModel.ParentCommentId = part.ParentCommentId.Text;
                viewModel.ContentItemId = part.ContentItemId.Text;
                viewModel.ContentType = part.ContentType.Text;
                viewModel.IsApproved = part.IsApproved.Value;
                viewModel.IsSpam = part.IsSpam.Value;
                viewModel.CommentDate = part.CommentDate.Value ?? DateTime.UtcNow;
                viewModel.IpAddress = part.IpAddress.Text;
                viewModel.UserAgent = part.UserAgent.Text;
                viewModel.Rating = part.Rating.Value ?? 0;
                viewModel.Status = part.Status.Text;
                viewModel.ContentItem = part.ContentItem;
            })
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10");
        }

        public override IDisplayResult Edit(CommentPart part, BuildPartEditorContext context)
        {
            return Initialize<CommentPartViewModel>(GetEditorShapeType(context), viewModel =>
            {
                viewModel.CommentId = part.CommentId.Text;
                viewModel.AuthorName = part.AuthorName.Text;
                viewModel.AuthorEmail = part.AuthorEmail.Text;
                viewModel.AuthorWebsite = part.AuthorWebsite.Text;
                viewModel.CommentContent = part.CommentContent.Html;
                viewModel.ParentCommentId = part.ParentCommentId.Text;
                viewModel.ContentItemId = part.ContentItemId.Text;
                viewModel.ContentType = part.ContentType.Text;
                viewModel.IsApproved = part.IsApproved.Value;
                viewModel.IsSpam = part.IsSpam.Value;
                viewModel.CommentDate = part.CommentDate.Value ?? DateTime.UtcNow;
                viewModel.IpAddress = part.IpAddress.Text;
                viewModel.UserAgent = part.UserAgent.Text;
                viewModel.Rating = part.Rating.Value ?? 0;
                viewModel.Status = part.Status.Text;
                viewModel.ContentItem = part.ContentItem;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(CommentPart part, UpdatePartEditorContext context)
        {
            var viewModel = new CommentPartViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.CommentId.Text = viewModel.CommentId;
                part.AuthorName.Text = viewModel.AuthorName;
                part.AuthorEmail.Text = viewModel.AuthorEmail;
                part.AuthorWebsite.Text = viewModel.AuthorWebsite;
                part.CommentContent.Html = viewModel.CommentContent;
                part.ParentCommentId.Text = viewModel.ParentCommentId;
                part.ContentItemId.Text = viewModel.ContentItemId;
                part.ContentType.Text = viewModel.ContentType;
                part.IsApproved.Value = viewModel.IsApproved;
                part.IsSpam.Value = viewModel.IsSpam;
                part.CommentDate.Value = viewModel.CommentDate;
                part.IpAddress.Text = viewModel.IpAddress;
                part.UserAgent.Text = viewModel.UserAgent;
                part.Rating.Value = viewModel.Rating;
                part.Status.Text = viewModel.Status;
            }

            return Edit(part, context);
        }
    }
}