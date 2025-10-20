using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace HoangNgoc.Comment.Models
{
    public class CommentPart : ContentPart
    {
        public TextField CommentId { get; set; } = new();
        public TextField AuthorName { get; set; } = new();
        public TextField AuthorEmail { get; set; } = new();
        public TextField AuthorWebsite { get; set; } = new();
        public HtmlField CommentContent { get; set; } = new();
        public TextField ParentCommentId { get; set; } = new();
        public TextField ContentItemId { get; set; } = new();
        public TextField ContentType { get; set; } = new();
        public BooleanField IsApproved { get; set; } = new();
        public BooleanField IsSpam { get; set; } = new();
        public DateTimeField CommentDate { get; set; } = new();
        public TextField IpAddress { get; set; } = new();
        public TextField UserAgent { get; set; } = new();
        public NumericField Rating { get; set; } = new();
        public TextField Status { get; set; } = new();
    }
}