using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using HoangNgoc.Training.Models;
using HoangNgoc.Training.Indexes;
using Microsoft.Extensions.Logging;

namespace HoangNgoc.Training.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IContentManager _contentManager;
        private readonly YesSql.ISession _session;
        private readonly ILogger<TrainingService> _logger;

        public TrainingService(
            IContentManager contentManager,
            YesSql.ISession session,
            ILogger<TrainingService> logger)
        {
            _contentManager = contentManager;
            _session = session;
            _logger = logger;
        }

        public async Task<IEnumerable<ContentItem>> GetCoursesAsync(int skip = 0, int take = 10)
        {
            var courses = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "Course" && x.Published)
                .OrderByDescending(x => x.CreatedUtc)
                .Skip(skip)
                .Take(take)
                .ListAsync();

            return courses;
        }

        public async Task<IEnumerable<ContentItem>> GetFeaturedCoursesAsync(int take = 5)
        {
            var courses = await _session.Query<ContentItem, CourseIndex>()
                .Where(x => x.IsFeatured && x.IsActive)
                .OrderByDescending(x => x.CreatedUtc)
                .Take(take)
                .ListAsync();

            return courses;
        }

        public async Task<ContentItem?> GetCourseByIdAsync(string contentItemId)
        {
            return await _contentManager.GetAsync(contentItemId);
        }

        public async Task<ContentItem?> GetCourseBySlugAsync(string slug)
        {
            var course = await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "Course" && x.Published)
                .FirstOrDefaultAsync();

            return course;
        }

        public async Task<IEnumerable<ContentItem>> GetCoursesByCategoryAsync(string categoryId, int skip = 0, int take = 10)
        {
            var courses = await _session.Query<ContentItem, CourseIndex>()
                .Where(x => x.Category == categoryId && x.IsActive)
                .OrderByDescending(x => x.CreatedUtc)
                .Skip(skip)
                .Take(take)
                .ListAsync();

            return courses;
        }

        public async Task<IEnumerable<ContentItem>> SearchCoursesAsync(string searchTerm, int skip = 0, int take = 10)
        {
            var courses = await _session.Query<ContentItem, CourseIndex>()
                .Where(x => x.Title.Contains(searchTerm) || x.Description.Contains(searchTerm))
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedUtc)
                .Skip(skip)
                .Take(take)
                .ListAsync();

            return courses;
        }

        public async Task<IEnumerable<ContentItem>> GetLessonsByCourseAsync(string courseId)
        {
            var lessons = await _session.Query<ContentItem, LessonIndex>()
                .Where(x => x.CourseId == courseId && x.IsActive)
                .OrderBy(x => x.SortOrder)
                .ListAsync();

            return lessons;
        }

        public async Task<ContentItem?> GetLessonByIdAsync(string contentItemId)
        {
            return await _contentManager.GetAsync(contentItemId);
        }

        public async Task<ContentItem?> GetNextLessonAsync(string courseId, int currentLessonNumber)
        {
            var nextLesson = await _session.Query<ContentItem, LessonIndex>()
                .Where(x => x.CourseId == courseId && x.LessonNumber > currentLessonNumber && x.IsActive)
                .OrderBy(x => x.LessonNumber)
                .FirstOrDefaultAsync();

            return nextLesson;
        }

        public async Task<ContentItem?> GetPreviousLessonAsync(string courseId, int currentLessonNumber)
        {
            var previousLesson = await _session.Query<ContentItem, LessonIndex>()
                .Where(x => x.CourseId == courseId && x.LessonNumber < currentLessonNumber && x.IsActive)
                .OrderByDescending(x => x.LessonNumber)
                .FirstOrDefaultAsync();

            return previousLesson;
        }

        public async Task<ContentItem?> EnrollStudentAsync(string courseId, string studentId, string studentName, string studentEmail)
        {
            try
            {
                var enrollment = await _contentManager.NewAsync("Enrollment");
                var enrollmentPart = enrollment.As<EnrollmentPart>();
                
                enrollmentPart.Course.ContentItemIds = new[] { courseId };
                enrollmentPart.StudentId.Text = studentId;
                enrollmentPart.StudentName.Text = studentName;
                enrollmentPart.StudentEmail.Text = studentEmail;
                enrollmentPart.EnrollmentDate.Value = DateTime.UtcNow;
                enrollmentPart.Progress.Value = 0;
                enrollmentPart.Status.Text = "Active";
                enrollmentPart.PaymentStatus.Text = "Pending";

                await _contentManager.CreateAsync(enrollment);
                await _contentManager.PublishAsync(enrollment);

                return enrollment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enrolling student {StudentId} in course {CourseId}", studentId, courseId);
                return null;
            }
        }

        public async Task<ContentItem?> GetEnrollmentAsync(string courseId, string studentId)
        {
            var enrollment = await _session.Query<ContentItem, EnrollmentIndex>()
                .Where(x => x.CourseId == courseId && x.StudentId == studentId)
                .FirstOrDefaultAsync();

            return enrollment;
        }

        public async Task<bool> UpdateProgressAsync(string enrollmentId, int progress)
        {
            try
            {
                var enrollment = await _contentManager.GetAsync(enrollmentId);
                if (enrollment == null) return false;

                var enrollmentPart = enrollment.As<EnrollmentPart>();
                enrollmentPart.Progress.Value = progress;

                await _contentManager.UpdateAsync(enrollment);
                await _contentManager.PublishAsync(enrollment);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating progress for enrollment {EnrollmentId}", enrollmentId);
                return false;
            }
        }

        public async Task<bool> CompleteEnrollmentAsync(string enrollmentId, int finalScore)
        {
            try
            {
                var enrollment = await _contentManager.GetAsync(enrollmentId);
                if (enrollment == null) return false;

                var enrollmentPart = enrollment.As<EnrollmentPart>();
                enrollmentPart.Progress.Value = 100;
                enrollmentPart.FinalScore.Value = finalScore;
                enrollmentPart.Status.Text = "Completed";
                enrollmentPart.CompletionDate.Value = DateTime.UtcNow;

                if (finalScore >= 70) // Passing score
                {
                    enrollmentPart.IsCertified.Value = true;
                    enrollmentPart.CertificationDate.Value = DateTime.UtcNow;
                    enrollmentPart.CertificateNumber.Text = $"CERT-{DateTime.UtcNow:yyyyMMdd}-{enrollmentId[..8]}";
                }

                await _contentManager.UpdateAsync(enrollment);
                await _contentManager.PublishAsync(enrollment);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing enrollment {EnrollmentId}", enrollmentId);
                return false;
            }
        }

        public async Task<bool> IsStudentEnrolledAsync(string courseId, string studentId)
        {
            var enrollment = await GetEnrollmentAsync(courseId, studentId);
            return enrollment != null;
        }

        public async Task<IEnumerable<ContentItem>> GetStudentEnrollmentsAsync(string studentId)
        {
            var enrollments = await _session.Query<ContentItem, EnrollmentIndex>()
                .Where(x => x.StudentId == studentId)
                .OrderByDescending(x => x.EnrollmentDate)
                .ListAsync();

            return enrollments;
        }

        public async Task<int> GetCourseCountAsync()
        {
            return await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "Course" && x.Published)
                .CountAsync();
        }

        public async Task<int> GetActiveStudentsCountAsync()
        {
            return await _session.Query<ContentItem, EnrollmentIndex>()
                .Where(x => x.Status == "Active")
                .CountAsync();
        }

        public async Task<int> GetCompletedCoursesCountAsync()
        {
            return await _session.Query<ContentItem, EnrollmentIndex>()
                .Where(x => x.Status == "Completed")
                .CountAsync();
        }

        public async Task<decimal> GetAverageCompletionRateAsync()
        {
            var totalEnrollments = await _session.Query<ContentItem, EnrollmentIndex>().CountAsync();
            var completedEnrollments = await GetCompletedCoursesCountAsync();

            if (totalEnrollments == 0) return 0;

            return (decimal)completedEnrollments / totalEnrollments * 100;
        }
    }
}