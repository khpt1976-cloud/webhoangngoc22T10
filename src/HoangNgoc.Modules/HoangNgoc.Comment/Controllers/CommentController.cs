using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.Comment.Services;
using HoangNgoc.Comment.ViewModels;
using HoangNgoc.Comment.Models;

namespace HoangNgoc.Comment.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(string contentItemId = "", string status = "", string search = "", int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;
            IEnumerable<ContentItem> comments;

            if (!string.IsNullOrEmpty(search))
            {
                comments = await _commentService.SearchCommentsAsync(search, skip, pageSize);
            }
            else if (!string.IsNullOrEmpty(status))
            {
                comments = await _commentService.GetCommentsByStatusAsync(status, skip, pageSize);
            }
            else if (!string.IsNullOrEmpty(contentItemId))
            {
                comments = await _commentService.GetCommentsAsync(contentItemId, "", skip, pageSize);
            }
            else
            {
                comments = await _commentService.GetCommentsByStatusAsync("", skip, pageSize);
            }

            var viewModel = new CommentListViewModel
            {
                Comments = comments.Select(MapToViewModel),
                TotalComments = await _commentService.GetCommentCountAsync(contentItemId, status),
                Page = page,
                PageSize = pageSize,
                SearchTerm = search,
                StatusFilter = status,
                ContentTypeFilter = contentItemId
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(string contentItemId, string contentType, string parentCommentId = "")
        {
            var viewModel = new CommentFormViewModel
            {
                ContentItemId = contentItemId,
                ContentType = contentType,
                ParentCommentId = parentCommentId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Set IP address and user agent
                model.ContentItemId = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
                
                var comment = await _commentService.CreateCommentAsync(model);
                
                TempData["Success"] = "Comment submitted successfully and is pending approval.";
                return RedirectToAction("Details", "Content", new { contentItemId = model.ContentItemId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while submitting your comment.");
                return View(model);
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var comment = await _commentService.GetCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var viewModel = MapToViewModel(comment);
            var replies = await _commentService.GetRepliesAsync(id);
            
            ViewBag.Replies = replies.Select(MapToViewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Moderate(string id, string action)
        {
            var result = await _commentService.ModerateCommentAsync(id, action);
            
            if (result)
            {
                TempData["Success"] = $"Comment {action}d successfully.";
            }
            else
            {
                TempData["Error"] = $"Failed to {action} comment.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            
            if (result)
            {
                TempData["Success"] = "Comment deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete comment.";
            }

            return RedirectToAction("Index");
        }

        private static CommentPartViewModel MapToViewModel(ContentItem contentItem)
        {
            var commentPart = contentItem.As<CommentPart>();
            
            return new CommentPartViewModel
            {
                CommentId = commentPart.CommentId?.Text ?? "",
                AuthorName = commentPart.AuthorName?.Text ?? "",
                AuthorEmail = commentPart.AuthorEmail?.Text ?? "",
                AuthorWebsite = commentPart.AuthorWebsite?.Text ?? "",
                CommentContent = commentPart.CommentContent?.Html ?? "",
                ParentCommentId = commentPart.ParentCommentId?.Text ?? "",
                ContentItemId = commentPart.ContentItemId?.Text ?? "",
                ContentType = commentPart.ContentType?.Text ?? "",
                IsApproved = commentPart.IsApproved?.Value ?? false,
                IsSpam = commentPart.IsSpam?.Value ?? false,
                CommentDate = commentPart.CommentDate?.Value ?? DateTime.UtcNow,
                IpAddress = commentPart.IpAddress?.Text ?? "",
                UserAgent = commentPart.UserAgent?.Text ?? "",
                Rating = commentPart.Rating?.Value ?? 0,
                Status = commentPart.Status?.Text ?? "",
                ContentItem = contentItem
            };
        }
    }
}