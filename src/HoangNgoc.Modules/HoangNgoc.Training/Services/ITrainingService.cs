using OrchardCore.ContentManagement;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Services
{
    public interface ITrainingService
    {
        Task<IEnumerable<ContentItem>> GetCoursesAsync(int skip = 0, int take = 10);
        Task<IEnumerable<ContentItem>> GetFeaturedCoursesAsync(int take = 5);
        Task<ContentItem?> GetCourseByIdAsync(string contentItemId);
        Task<ContentItem?> GetCourseBySlugAsync(string slug);
        Task<IEnumerable<ContentItem>> GetCoursesByCategoryAsync(string categoryId, int skip = 0, int take = 10);
        Task<IEnumerable<ContentItem>> SearchCoursesAsync(string searchTerm, int skip = 0, int take = 10);
        
        Task<IEnumerable<ContentItem>> GetLessonsByCourseAsync(string courseId);
        Task<ContentItem?> GetLessonByIdAsync(string contentItemId);
        Task<ContentItem?> GetNextLessonAsync(string courseId, int currentLessonNumber);
        Task<ContentItem?> GetPreviousLessonAsync(string courseId, int currentLessonNumber);
        
        Task<ContentItem?> EnrollStudentAsync(string courseId, string studentId, string studentName, string studentEmail);
        Task<ContentItem?> GetEnrollmentAsync(string courseId, string studentId);
        Task<bool> UpdateProgressAsync(string enrollmentId, int progress);
        Task<bool> CompleteEnrollmentAsync(string enrollmentId, int finalScore);
        Task<bool> IsStudentEnrolledAsync(string courseId, string studentId);
        Task<IEnumerable<ContentItem>> GetStudentEnrollmentsAsync(string studentId);
        
        Task<int> GetCourseCountAsync();
        Task<int> GetActiveStudentsCountAsync();
        Task<int> GetCompletedCoursesCountAsync();
        Task<decimal> GetAverageCompletionRateAsync();
    }
}