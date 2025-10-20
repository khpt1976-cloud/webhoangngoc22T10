using OrchardCore.ContentManagement;
using HoangNgoc.Comment.ViewModels;

namespace HoangNgoc.Comment.Services
{
    public interface ICommentService
    {
        Task<ContentItem> CreateCommentAsync(CommentFormViewModel model);
        Task<ContentItem?> GetCommentAsync(string commentId);
        Task<IEnumerable<ContentItem>> GetCommentsAsync(string contentItemId, string contentType = "", int skip = 0, int take = 20);
        Task<IEnumerable<ContentItem>> GetCommentsByStatusAsync(string status, int skip = 0, int take = 20);
        Task<IEnumerable<ContentItem>> SearchCommentsAsync(string searchTerm, int skip = 0, int take = 20);
        Task<bool> UpdateCommentAsync(string commentId, CommentPartViewModel model);
        Task<bool> DeleteCommentAsync(string commentId);
        Task<bool> ApproveCommentAsync(string commentId);
        Task<bool> RejectCommentAsync(string commentId);
        Task<bool> MarkAsSpamAsync(string commentId);
        Task<bool> UnmarkAsSpamAsync(string commentId);
        Task<int> GetCommentCountAsync(string contentItemId = "", string status = "");
        Task<IEnumerable<ContentItem>> GetRepliesAsync(string parentCommentId);
        Task<bool> ModerateCommentAsync(string commentId, string action);
    }
}