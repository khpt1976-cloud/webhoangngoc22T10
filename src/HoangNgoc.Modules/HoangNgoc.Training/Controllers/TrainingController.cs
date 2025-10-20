using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Training.Services;
using HoangNgoc.Training.ViewModels;

namespace HoangNgoc.Training.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IContentManager _contentManager;

        public TrainingController(
            ITrainingService trainingService,
            IContentManager contentManager)
        {
            _trainingService = trainingService;
            _contentManager = contentManager;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, string? category = null, string? search = null)
        {
            var skip = (page - 1) * pageSize;
            IEnumerable<ContentItem> courses;

            if (!string.IsNullOrEmpty(search))
            {
                courses = await _trainingService.SearchCoursesAsync(search, skip, pageSize);
            }
            else if (!string.IsNullOrEmpty(category))
            {
                courses = await _trainingService.GetCoursesByCategoryAsync(category, skip, pageSize);
            }
            else
            {
                courses = await _trainingService.GetCoursesAsync(skip, pageSize);
            }

            var featuredCourses = await _trainingService.GetFeaturedCoursesAsync(6);

            var viewModel = new CourseListViewModel
            {
                Courses = courses,
                FeaturedCourses = featuredCourses,
                CurrentPage = page,
                PageSize = pageSize,
                Category = category,
                SearchTerm = search
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Course(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var course = await _trainingService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            var lessons = await _trainingService.GetLessonsByCourseAsync(id);

            var viewModel = new CourseDetailViewModel
            {
                Course = course,
                Lessons = lessons
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Lesson(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var lesson = await _trainingService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return View(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(string courseId, string studentId, string studentName, string studentEmail)
        {
            if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(studentId))
                return BadRequest("Missing required parameters");

            var isEnrolled = await _trainingService.IsStudentEnrolledAsync(courseId, studentId);
            if (isEnrolled)
                return BadRequest("Student is already enrolled in this course");

            var enrollment = await _trainingService.EnrollStudentAsync(courseId, studentId, studentName, studentEmail);
            if (enrollment == null)
                return BadRequest("Failed to enroll student");

            return Json(new { success = true, enrollmentId = enrollment.ContentItemId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProgress(string enrollmentId, int progress)
        {
            if (string.IsNullOrEmpty(enrollmentId))
                return BadRequest("Missing enrollment ID");

            var success = await _trainingService.UpdateProgressAsync(enrollmentId, progress);
            return Json(new { success });
        }

        [HttpPost]
        public async Task<IActionResult> CompleteCourse(string enrollmentId, int finalScore)
        {
            if (string.IsNullOrEmpty(enrollmentId))
                return BadRequest("Missing enrollment ID");

            var success = await _trainingService.CompleteEnrollmentAsync(enrollmentId, finalScore);
            return Json(new { success });
        }

        public async Task<IActionResult> MyEnrollments(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                return BadRequest("Missing student ID");

            var enrollments = await _trainingService.GetStudentEnrollmentsAsync(studentId);

            var viewModel = new StudentEnrollmentsViewModel
            {
                StudentId = studentId,
                Enrollments = enrollments
            };

            return View(viewModel);
        }
    }
}