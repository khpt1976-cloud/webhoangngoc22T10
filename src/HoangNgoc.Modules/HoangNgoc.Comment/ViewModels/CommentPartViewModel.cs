using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace HoangNgoc.Comment.ViewModels
{
    public class CommentPartViewModel
    {
        public string CommentId { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string AuthorWebsite { get; set; } = string.Empty;
        public string CommentContent { get; set; } = string.Empty;
        public string ParentCommentId { get; set; } = string.Empty;
        public string ContentItemId { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsSpam { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Status { get; set; } = "Pending";

        [BindNever]
        public ContentItem ContentItem { get; set; } = new();
    }

    public class CommentListViewModel
    {
        public IEnumerable<CommentPartViewModel> Comments { get; set; } = new List<CommentPartViewModel>();
        public int TotalComments { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SearchTerm { get; set; } = string.Empty;
        public string StatusFilter { get; set; } = string.Empty;
        public string ContentTypeFilter { get; set; } = string.Empty;
    }

    public class CommentFormViewModel
    {
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string AuthorWebsite { get; set; } = string.Empty;
        public string CommentContent { get; set; } = string.Empty;
        public string ParentCommentId { get; set; } = string.Empty;
        public string ContentItemId { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}