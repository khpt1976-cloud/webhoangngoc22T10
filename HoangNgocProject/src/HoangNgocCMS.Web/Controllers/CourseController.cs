using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using YesSql;
using HoangNgoc.Course.Services;
using HoangNgoc.Course.ViewModels;

namespace HoangNgocCMS.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly ISession _session;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseController(
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            ISession session,
            IUpdateModelAccessor updateModelAccessor,
            ICourseEnrollmentService courseEnrollmentService)
        {
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _session = session;
            _updateModelAccessor = updateModelAccessor;
            _courseEnrollmentService = courseEnrollmentService;
        }

        // GET: /courses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var course = await _contentManager.GetAsync(id);
            
            if (course == null || !course.Published || course.ContentType != "Course")
            {
                return NotFound();
            }

            // Build display shape
            var shape = await _contentItemDisplayManager.BuildDisplayAsync(course, _updateModelAccessor.ModelUpdater);

            // Get related courses
            var relatedCourses = await GetRelatedCoursesAsync(course);

            var viewModel = new CourseDetailsViewModel
            {
                Course = shape,
                RelatedCourses = relatedCourses,
                IsUserLoggedIn = User.Identity.IsAuthenticated,
                IsUserEnrolled = User.Identity.IsAuthenticated ? 
                    await _courseEnrollmentService.IsUserEnrolledAsync(id, User.Identity.Name) : false,
                IsInWishlist = User.Identity.IsAuthenticated ?
                    await _courseEnrollmentService.IsInUserWishlistAsync(id, User.Identity.Name) : false
            };

            return View(viewModel);
        }

        // POST: /api/courses/{id}/enroll
        [HttpPost]
        [Route("api/courses/{id}/enroll")]
        [Authorize]
        public async Task<IActionResult> Enroll(string id)
        {
            try
            {
                var course = await _contentManager.GetAsync(id);
                if (course == null || !course.Published || course.ContentType != "Course")
                {
                    return NotFound(new { success = false, message = "Course not found" });
                }

                var userId = User.Identity.Name;

                // Check if already enrolled
                if (await _courseEnrollmentService.IsUserEnrolledAsync(id, userId))
                {
                    return BadRequest(new { success = false, message = "You are already enrolled in this course" });
                }

                // Create enrollment
                var enrollmentId = await _courseEnrollmentService.EnrollUserAsync(new CourseEnrollmentCreateModel
                {
                    CourseId = id,
                    UserId = userId,
                    EnrollmentDate = DateTime.UtcNow
                });

                return Ok(new
                {
                    success = true,
                    enrollmentId = enrollmentId,
                    message = "Successfully enrolled in the course"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while enrolling in the course" });
            }
        }

        // POST: /api/courses/{id}/wishlist
        [HttpPost]
        [Route("api/courses/{id}/wishlist")]
        [Authorize]
        public async Task<IActionResult> ToggleWishlist(string id)
        {
            try
            {
                var course = await _contentManager.GetAsync(id);
                if (course == null || !course.Published || course.ContentType != "Course")
                {
                    return NotFound(new { success = false, message = "Course not found" });
                }

                var userId = User.Identity.Name;
                var isInWishlist = await _courseEnrollmentService.IsInUserWishlistAsync(id, userId);

                if (isInWishlist)
                {
                    await _courseEnrollmentService.RemoveFromWishlistAsync(userId, id);
                    return Ok(new { success = true, inWishlist = false, message = "Course removed from wishlist" });
                }
                else
                {
                    await _courseEnrollmentService.AddToWishlistAsync(userId, id);
                    return Ok(new { success = true, inWishlist = true, message = "Course added to wishlist" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while updating wishlist" });
            }
        }

        // Helper method to get related courses
        private async Task<List<dynamic>> GetRelatedCoursesAsync(ContentItem course)
        {
            var category = course.Content.Course.Category?.Text;
            var level = course.Content.Course.Level?.Text;

            var query = _session.Query<ContentItem>()
                .Where(x => x.ContentType == "Course" && 
                           x.Published && 
                           x.ContentItemId != course.ContentItemId);

            // Prefer same category or level
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Content.Course.Category.Text == category);
            }
            else if (!string.IsNullOrEmpty(level))
            {
                query = query.Where(x => x.Content.Course.Level.Text == level);
            }

            var relatedCourses = await query.Take(3).ListAsync();
            var shapes = new List<dynamic>();

            foreach (var relatedCourse in relatedCourses)
            {
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(relatedCourse, _updateModelAccessor.ModelUpdater, "Summary");
                shapes.Add(shape);
            }

            return shapes;
        }
    }
}