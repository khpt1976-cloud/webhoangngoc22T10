using OrchardCore.ContentManagement;

namespace HoangNgoc.Training.ViewModels
{
    public class StudentEnrollmentsViewModel
    {
        public string StudentId { get; set; } = string.Empty;
        public IEnumerable<ContentItem> Enrollments { get; set; } = new List<ContentItem>();
        public int TotalEnrollments { get; set; }
        public int CompletedCourses { get; set; }
        public int ActiveCourses { get; set; }
        public decimal AverageProgress { get; set; }
    }
}