using OrchardCore.ContentManagement;

namespace HoangNgoc.Training.ViewModels
{
    public class CourseDetailViewModel
    {
        public ContentItem Course { get; set; } = new();
        public IEnumerable<ContentItem> Lessons { get; set; } = new List<ContentItem>();
        public bool IsEnrolled { get; set; }
        public ContentItem? Enrollment { get; set; }
        public int Progress { get; set; }
        public string? StudentId { get; set; }
    }
}