using OrchardCore.Media.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.NewsArticle.Models
{
    public class NewsArticle : ContentPart
    {
        public TextField Title { get; set; } = new();
        public TextField Slug { get; set; } = new();
        public new HtmlField Content { get; set; } = new();
        public TextField Summary { get; set; } = new();
        public TextField Category { get; set; } = new();
        public TextField Tags { get; set; } = new();
        public TextField Author { get; set; } = new();
        public TextField AuthorId { get; set; } = new();
        public MediaField FeaturedImage { get; set; } = new();
        public TextField FeaturedImageAlt { get; set; } = new();
        public MediaField Gallery { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public BooleanField IsPublished { get; set; } = new();
        public BooleanField AllowComments { get; set; } = new();
        public DateTimeField PublishedDate { get; set; } = new();
        public DateTimeField CreatedDate { get; set; } = new();
        public DateTimeField LastModified { get; set; } = new();
        public NumericField ViewCount { get; set; } = new();
        public NumericField LikeCount { get; set; } = new();
        public NumericField ShareCount { get; set; } = new();
        public NumericField CommentCount { get; set; } = new();
        public TextField MetaTitle { get; set; } = new();
        public TextField MetaDescription { get; set; } = new();
        public TextField MetaKeywords { get; set; } = new();
        public TextField Status { get; set; } = new(); // Draft, Published, Archived
        public NumericField ReadingTime { get; set; } = new(); // in minutes
        public TextField Language { get; set; } = new();
        public BooleanField IsBreaking { get; set; } = new();
        public NumericField Priority { get; set; } = new(); // 1-10
    }

    public class ArticleComment : ContentPart
    {
        public TextField ArticleId { get; set; } = new();
        public TextField AuthorId { get; set; } = new();
        public TextField AuthorName { get; set; } = new();
        public TextField AuthorEmail { get; set; } = new();
        public new HtmlField Content { get; set; } = new();
        public TextField ParentCommentId { get; set; } = new(); // for replies
        public DateTimeField CreatedDate { get; set; } = new();
        public BooleanField IsApproved { get; set; } = new();
        public DateTimeField ApprovedDate { get; set; } = new();
        public TextField ApprovedBy { get; set; } = new();
        public BooleanField IsSpam { get; set; } = new();
        public NumericField LikeCount { get; set; } = new();
        public NumericField DislikeCount { get; set; } = new();
        public TextField Status { get; set; } = new(); // Pending, Approved, Rejected, Spam
        public TextField IpAddress { get; set; } = new();
        public TextField UserAgent { get; set; } = new();
    }

    public class ArticleRating : ContentPart
    {
        public TextField ArticleId { get; set; } = new();
        public TextField UserId { get; set; } = new();
        public NumericField Rating { get; set; } = new(); // 1-5 stars
        public DateTimeField RatedDate { get; set; } = new();
        public TextField IpAddress { get; set; } = new();
    }
}