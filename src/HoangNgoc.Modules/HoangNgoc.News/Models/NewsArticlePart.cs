using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;
using OrchardCore.Media.Fields;

namespace HoangNgoc.News.Models;

public class NewsArticlePart : ContentPart
{
    public TextField Summary { get; set; } = new();
    public TextField Author { get; set; } = new();
    public BooleanField IsFeatured { get; set; } = new();
    public NumericField ViewCount { get; set; } = new();
    public DateTimeField PublishedDate { get; set; } = new();
    public MediaField FeaturedImage { get; set; } = new();
}