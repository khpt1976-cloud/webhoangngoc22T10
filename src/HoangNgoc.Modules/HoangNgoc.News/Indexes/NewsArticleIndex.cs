using OrchardCore.ContentManagement;
using OrchardCore.Data;
using YesSql.Indexes;
using HoangNgoc.News.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Taxonomies.Fields;

namespace HoangNgoc.News.Indexes
{
    public class NewsArticleIndex : MapIndex
    {
        public string ContentItemId { get; set; } = "";
        public string Title { get; set; } = "";
        public string Summary { get; set; } = "";
        public string Content { get; set; } = "";
        public string Category { get; set; } = "";
        public string Tags { get; set; } = "";
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
        public DateTime? PublishedUtc { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool Published { get; set; }
        public bool Latest { get; set; }
        public string Author { get; set; } = "";
    }

    public class NewsArticleIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<NewsArticleIndex>()
                .Map(contentItem =>
                {
                    if (contentItem.ContentType != "NewsArticle")
                        return null;

                    var newsArticlePart = contentItem.As<NewsArticlePart>();
                    if (newsArticlePart == null)
                        return null;

                    // Get fields from ContentItem
                    var titleField = contentItem.Content.NewsArticlePart?.Title as TextField;
                    var contentField = contentItem.Content.NewsArticlePart?.Content as HtmlField;
                    var categoryField = contentItem.Content.NewsArticlePart?.Category as TaxonomyField;
                    var tagsField = contentItem.Content.NewsArticlePart?.Tags as TaxonomyField;

                    return new NewsArticleIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        Title = titleField?.Text ?? "",
                        Summary = newsArticlePart.Summary?.Text ?? "",
                        Content = contentField?.Html ?? "",
                        Category = categoryField?.TermContentItemIds?.FirstOrDefault() ?? "",
                        Tags = string.Join(",", tagsField?.TermContentItemIds ?? new string[0]),
                        IsFeatured = newsArticlePart.IsFeatured?.Value ?? false,
                        ViewCount = (int)(newsArticlePart.ViewCount?.Value ?? 0),
                        PublishedUtc = contentItem.PublishedUtc,
                        CreatedUtc = contentItem.CreatedUtc ?? DateTime.UtcNow,
                        ModifiedUtc = contentItem.ModifiedUtc ?? DateTime.UtcNow,
                        Published = contentItem.Published,
                        Latest = contentItem.Latest,
                        Author = contentItem.Author ?? ""
                    };
                });
        }
    }
}