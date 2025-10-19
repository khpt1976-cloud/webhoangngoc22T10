using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using HoangNgoc.Comment.Models;
using HoangNgoc.Comment.ViewModels;
using HoangNgoc.Comment.Indexes;

namespace HoangNgoc.Comment.Services
{
    public class CommentService : ICommentService
    {
        private readonly IContentManager _contentManager;
        private readonly YesSql.ISession _session;

        public CommentService(IContentManager contentManager, YesSql.ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public async Task<ContentItem> CreateCommentAsync(CommentFormViewModel model)
        {
            var contentItem = await _contentManager.NewAsync("Comment");
            var commentPart = contentItem.As<CommentPart>();

            commentPart.CommentId.Text = Guid.NewGuid().ToString();
            commentPart.AuthorName.Text = model.AuthorName;
            commentPart.AuthorEmail.Text = model.AuthorEmail;
            commentPart.AuthorWebsite.Text = model.AuthorWebsite;
            commentPart.CommentContent.Html = model.CommentContent;
            commentPart.ParentCommentId.Text = model.ParentCommentId;
            commentPart.ContentItemId.Text = model.ContentItemId;
            commentPart.ContentType.Text = model.ContentType;
            commentPart.IsApproved.Value = false; // Default to pending approval
            commentPart.IsSpam.Value = false;
            commentPart.CommentDate.Value = DateTime.UtcNow;
            commentPart.Rating.Value = model.Rating;
            commentPart.Status.Text = "Pending";

            contentItem.Apply(commentPart);
            await _contentManager.CreateAsync(contentItem);

            return contentItem;
        }

        public async Task<ContentItem?> GetCommentAsync(string commentId)
        {
            var comments = await _session.Query<ContentItem, CommentIndex>()
                .Where(x => x.CommentId == commentId)
                .ListAsync();

            return comments.FirstOrDefault();
        }

        public async Task<IEnumerable<ContentItem>> GetCommentsAsync(string contentItemId, string contentType = "", int skip = 0, int take = 20)
        {
            var query = _session.Query<ContentItem, CommentIndex>()
                .Where(x => x.ContentItemId == contentItemId);

            if (!string.IsNullOrEmpty(contentType))
            {
                query = query.Where(x => x.ContentType == contentType);
            }

            return await query
                .OrderByDescending(x => x.CommentDate)
                .Skip(skip)
                .Take(take)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetCommentsByStatusAsync(string status, int skip = 0, int take = 20)
        {
            return await _session.Query<ContentItem, CommentIndex>()
                .Where(x => x.Status == status)
                .OrderByDescending(x => x.CommentDate)
                .Skip(skip)
                .Take(take)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> SearchCommentsAsync(string searchTerm, int skip = 0, int take = 20)
        {
            return await _session.Query<ContentItem, CommentIndex>()
                .Where(x => x.AuthorName.Contains(searchTerm) || 
                           x.CommentContent.Contains(searchTerm) ||
                           x.AuthorEmail.Contains(searchTerm))
                .OrderByDescending(x => x.CommentDate)
                .Skip(skip)
                .Take(take)
                .ListAsync();
        }

        public async Task<bool> UpdateCommentAsync(string commentId, CommentPartViewModel model)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            var commentPart = contentItem.As<CommentPart>();
            commentPart.AuthorName.Text = model.AuthorName;
            commentPart.AuthorEmail.Text = model.AuthorEmail;
            commentPart.AuthorWebsite.Text = model.AuthorWebsite;
            commentPart.CommentContent.Html = model.CommentContent;
            commentPart.IsApproved.Value = model.IsApproved;
            commentPart.IsSpam.Value = model.IsSpam;
            commentPart.Rating.Value = model.Rating;
            commentPart.Status.Text = model.Status;

            contentItem.Apply(commentPart);
            await _contentManager.UpdateAsync(contentItem);

            return true;
        }

        public async Task<bool> DeleteCommentAsync(string commentId)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            await _contentManager.RemoveAsync(contentItem);
            return true;
        }

        public async Task<bool> ApproveCommentAsync(string commentId)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            var commentPart = contentItem.As<CommentPart>();
            commentPart.IsApproved.Value = true;
            commentPart.Status.Text = "Approved";

            contentItem.Apply(commentPart);
            await _contentManager.UpdateAsync(contentItem);

            return true;
        }

        public async Task<bool> RejectCommentAsync(string commentId)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            var commentPart = contentItem.As<CommentPart>();
            commentPart.IsApproved.Value = false;
            commentPart.Status.Text = "Rejected";

            contentItem.Apply(commentPart);
            await _contentManager.UpdateAsync(contentItem);

            return true;
        }

        public async Task<bool> MarkAsSpamAsync(string commentId)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            var commentPart = contentItem.As<CommentPart>();
            commentPart.IsSpam.Value = true;
            commentPart.Status.Text = "Spam";

            contentItem.Apply(commentPart);
            await _contentManager.UpdateAsync(contentItem);

            return true;
        }

        public async Task<bool> UnmarkAsSpamAsync(string commentId)
        {
            var contentItem = await GetCommentAsync(commentId);
            if (contentItem == null) return false;

            var commentPart = contentItem.As<CommentPart>();
            commentPart.IsSpam.Value = false;
            commentPart.Status.Text = "Pending";

            contentItem.Apply(commentPart);
            await _contentManager.UpdateAsync(contentItem);

            return true;
        }

        public async Task<int> GetCommentCountAsync(string contentItemId = "", string status = "")
        {
            var query = _session.Query<ContentItem, CommentIndex>();

            if (!string.IsNullOrEmpty(contentItemId))
            {
                query = query.Where(x => x.ContentItemId == contentItemId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetRepliesAsync(string parentCommentId)
        {
            return await _session.Query<ContentItem, CommentIndex>()
                .Where(x => x.ParentCommentId == parentCommentId)
                .OrderBy(x => x.CommentDate)
                .ListAsync();
        }

        public async Task<bool> ModerateCommentAsync(string commentId, string action)
        {
            return action.ToLower() switch
            {
                "approve" => await ApproveCommentAsync(commentId),
                "reject" => await RejectCommentAsync(commentId),
                "spam" => await MarkAsSpamAsync(commentId),
                "unspam" => await UnmarkAsSpamAsync(commentId),
                "delete" => await DeleteCommentAsync(commentId),
                _ => false
            };
        }
    }
}