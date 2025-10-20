using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using HoangNgoc.Comment.Services;
using HoangNgoc.Comment.ViewModels;
using HoangNgoc.Comment.Models;

namespace HoangNgoc.Comment.Controllers
{
    [Admin]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICommentService _commentService;

        public AdminController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(string status = "", string search = "", int page = 1, int pageSize = 20)
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
            else
            {
                comments = await _commentService.GetCommentsByStatusAsync("", skip, pageSize);
            }

            var viewModel = new CommentListViewModel
            {
                Comments = comments.Select(MapToViewModel),
                TotalComments = await _commentService.GetCommentCountAsync("", status),
                Page = page,
                PageSize = pageSize,
                SearchTerm = search,
                StatusFilter = status
            };

            return View(viewModel);
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var comment = await _commentService.GetCommentAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var viewModel = MapToViewModel(comment);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CommentPartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _commentService.UpdateCommentAsync(id, model);
            
            if (result)
            {
                TempData["Success"] = "Comment updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update comment.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string id)
        {
            var result = await _commentService.ApproveCommentAsync(id);
            
            if (result)
            {
                TempData["Success"] = "Comment approved successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to approve comment.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(string id)
        {
            var result = await _commentService.RejectCommentAsync(id);
            
            if (result)
            {
                TempData["Success"] = "Comment rejected successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to reject comment.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsSpam(string id)
        {
            var result = await _commentService.MarkAsSpamAsync(id);
            
            if (result)
            {
                TempData["Success"] = "Comment marked as spam successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to mark comment as spam.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UnmarkAsSpam(string id)
        {
            var result = await _commentService.UnmarkAsSpamAsync(id);
            
            if (result)
            {
                TempData["Success"] = "Comment unmarked as spam successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to unmark comment as spam.";
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

        [HttpPost]
        public async Task<IActionResult> BulkAction(string[] selectedComments, string action)
        {
            if (selectedComments == null || selectedComments.Length == 0)
            {
                TempData["Error"] = "No comments selected.";
                return RedirectToAction("Index");
            }

            var successCount = 0;
            foreach (var commentId in selectedComments)
            {
                var result = await _commentService.ModerateCommentAsync(commentId, action);
                if (result) successCount++;
            }

            TempData["Success"] = $"{successCount} comments {action}d successfully.";
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