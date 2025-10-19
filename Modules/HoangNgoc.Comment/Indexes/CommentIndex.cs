using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Comment.Models;

namespace HoangNgoc.Comment.Indexes
{
    public class CommentIndex : MapIndex
    {
        public string CommentId { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public string CommentContent { get; set; } = string.Empty;
        public string ParentCommentId { get; set; } = string.Empty;
        public string ContentItemId { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public bool IsSpam { get; set; }
        public DateTime CommentDate { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ContentItemVersionId { get; set; } = string.Empty;
        public bool Published { get; set; }
        public bool Latest { get; set; }
    }

    public class CommentIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<CommentIndex>()
                .Map(contentItem =>
                {
                    var commentPart = contentItem.As<CommentPart>();
                    if (commentPart == null) return null;

                    return new CommentIndex
                    {
                        CommentId = commentPart.CommentId?.Text ?? string.Empty,
                        AuthorName = commentPart.AuthorName?.Text ?? string.Empty,
                        AuthorEmail = commentPart.AuthorEmail?.Text ?? string.Empty,
                        CommentContent = commentPart.CommentContent?.Html ?? string.Empty,
                        ParentCommentId = commentPart.ParentCommentId?.Text ?? string.Empty,
                        ContentItemId = commentPart.ContentItemId?.Text ?? string.Empty,
                        ContentType = commentPart.ContentType?.Text ?? string.Empty,
                        IsApproved = commentPart.IsApproved?.Value ?? false,
                        IsSpam = commentPart.IsSpam?.Value ?? false,
                        CommentDate = commentPart.CommentDate?.Value ?? DateTime.UtcNow,
                        IpAddress = commentPart.IpAddress?.Text ?? string.Empty,
                        Rating = commentPart.Rating?.Value ?? 0,
                        Status = commentPart.Status?.Text ?? string.Empty,
                        ContentItemVersionId = contentItem.ContentItemVersionId,
                        Published = contentItem.Published,
                        Latest = contentItem.Latest
                    };
                });
        }
    }
}