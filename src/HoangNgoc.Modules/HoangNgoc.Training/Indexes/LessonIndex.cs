using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Indexes
{
    public class LessonIndex : MapIndex
    {
        public string ContentItemId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int LessonNumber { get; set; }
        public string CourseId { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public bool IsPreview { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool Published { get; set; }
    }

    public class LessonIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<LessonIndex>()
                .Map(contentItem =>
                {
                    var lessonPart = contentItem.As<LessonPart>();
                    if (lessonPart == null) return null;

                    return new LessonIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        Title = contentItem.DisplayText ?? string.Empty,
                        LessonNumber = int.TryParse(lessonPart.LessonNumber?.Text, out var num) ? num : 0,
                        CourseId = lessonPart.Course?.ContentItemIds?.FirstOrDefault() ?? string.Empty,
                        Duration = lessonPart.Duration?.Text ?? string.Empty,
                        VideoUrl = lessonPart.VideoUrl?.Text ?? string.Empty,
                        IsPreview = lessonPart.IsPreview?.Value ?? false,
                        IsActive = lessonPart.IsActive?.Value ?? false,
                        SortOrder = (int)(lessonPart.SortOrder?.Value ?? 0),
                        CreatedUtc = contentItem.CreatedUtc ?? DateTime.UtcNow,
                        ModifiedUtc = contentItem.ModifiedUtc ?? DateTime.UtcNow,
                        Published = contentItem.Published
                    };
                });
        }
    }
}