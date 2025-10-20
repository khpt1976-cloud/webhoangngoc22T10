using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Indexes
{
    public class CourseIndex : MapIndex
    {
        public string ContentItemId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxStudents { get; set; }
        public int CurrentStudents { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool Published { get; set; }
    }

    public class CourseIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<CourseIndex>()
                .Map(contentItem =>
                {
                    var coursePart = contentItem.As<CoursePart>();
                    if (coursePart == null) return null;

                    return new CourseIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        Title = contentItem.DisplayText ?? string.Empty,
                        CourseCode = coursePart.CourseCode?.Text ?? string.Empty,
                        Description = coursePart.Description?.Html ?? string.Empty,
                        Duration = coursePart.Duration?.Text ?? string.Empty,
                        Level = coursePart.Level?.Text ?? string.Empty,
                        Price = coursePart.Price?.Value ?? 0,
                        MaxStudents = (int)(coursePart.MaxStudents?.Value ?? 0),
                        CurrentStudents = (int)(coursePart.CurrentStudents?.Value ?? 0),
                        IsActive = coursePart.IsActive?.Value ?? false,
                        IsFeatured = coursePart.IsFeatured?.Value ?? false,
                        StartDate = coursePart.StartDate?.Value,
                        EndDate = coursePart.EndDate?.Value,
                        Category = coursePart.Category?.TermContentItemIds?.FirstOrDefault() ?? string.Empty,
                        Tags = string.Join(",", coursePart.Tags?.TermContentItemIds ?? Array.Empty<string>()),
                        Instructor = coursePart.Instructor?.Text ?? string.Empty,
                        CreatedUtc = contentItem.CreatedUtc ?? DateTime.UtcNow,
                        ModifiedUtc = contentItem.ModifiedUtc ?? DateTime.UtcNow,
                        Published = contentItem.Published
                    };
                });
        }
    }
}