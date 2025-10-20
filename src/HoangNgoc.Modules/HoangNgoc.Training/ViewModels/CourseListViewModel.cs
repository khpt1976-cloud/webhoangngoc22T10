using OrchardCore.ContentManagement;

namespace HoangNgoc.Training.ViewModels
{
    public class CourseListViewModel
    {
        public IEnumerable<ContentItem> Courses { get; set; } = new List<ContentItem>();
        public IEnumerable<ContentItem> FeaturedCourses { get; set; } = new List<ContentItem>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalPages { get; set; }
        public int TotalCourses { get; set; }
        public string? Category { get; set; }
        public string? SearchTerm { get; set; }
    }
}