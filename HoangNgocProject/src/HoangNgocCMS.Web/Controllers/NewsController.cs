using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using YesSql;
using HoangNgoc.NewsArticle.Services;
using HoangNgoc.NewsArticle.ViewModels;

namespace HoangNgocCMS.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly ISession _session;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly ICommentService _commentService;
        private readonly IArticleRatingService _articleRatingService;

        public NewsController(
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            ISession session,
            IUpdateModelAccessor updateModelAccessor,
            ICommentService commentService,
            IArticleRatingService articleRatingService)
        {
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _session = session;
            _updateModelAccessor = updateModelAccessor;
            _commentService = commentService;
            _articleRatingService = articleRatingService;
        }

        // GET: /news/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var article = await _contentManager.GetAsync(id);
            
            if (article == null || !article.Published || article.ContentType != "NewsArticle")
            {
                return NotFound();
            }

            // Build display shape
            var shape = await _contentItemDisplayManager.BuildDisplayAsync(article, _updateModelAccessor.ModelUpdater);

            // Get related articles
            var relatedArticles = await GetRelatedArticlesAsync(article);

            // Get comments
            var comments = await _commentService.GetArticleCommentsAsync(id);

            // Get article rating
            var rating = await _articleRatingService.GetArticleRatingAsync(id);
            var userRating = User.Identity.IsAuthenticated ? 
                await _articleRatingService.GetUserRatingAsync(id, User.Identity.Name) : 0;

            var viewModel = new NewsArticleDetailsViewModel
            {
                Article = shape,
                RelatedArticles = relatedArticles,
                Comments = comments,
                CommentsCount = comments.Count,
                IsUserLoggedIn = User.Identity.IsAuthenticated,
                AverageRating = rating.AverageRating,
                TotalRatings = rating.TotalRatings,
                UserRating = userRating
            };

            return View(viewModel);
        }

        // POST: /api/comments
        [HttpPost]
        [Route("api/comments")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid comment data" });
            }

            try
            {
                var article = await _contentManager.GetAsync(model.ArticleId);
                if (article == null || !article.Published || article.ContentType != "NewsArticle")
                {
                    return NotFound(new { success = false, message = "Article not found" });
                }

                var userId = User.Identity.Name;
                var userName = User.Identity.Name; // You might want to get the actual display name

                var commentId = await _commentService.AddCommentAsync(new CommentCreateModel
                {
                    ArticleId = model.ArticleId,
                    UserId = userId,
                    UserName = userName,
                    Content = model.Content,
                    ParentCommentId = model.ParentCommentId,
                    CreatedDate = DateTime.UtcNow
                });

                var comment = await _commentService.GetCommentAsync(commentId);

                return Ok(new
                {
                    success = true,
                    comment = new
                    {
                        id = comment.Id,
                        content = comment.Content,
                        userName = comment.UserName,
                        createdDate = comment.CreatedDate,
                        likesCount = 0,
                        isLiked = false
                    },
                    message = "Comment added successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while adding the comment" });
            }
        }

        // POST: /api/comments/{id}/reply
        [HttpPost]
        [Route("api/comments/{id}/reply")]
        [Authorize]
        public async Task<IActionResult> ReplyToComment(string id, [FromBody] ReplyCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid reply data" });
            }

            try
            {
                var parentComment = await _commentService.GetCommentAsync(id);
                if (parentComment == null)
                {
                    return NotFound(new { success = false, message = "Parent comment not found" });
                }

                var userId = User.Identity.Name;
                var userName = User.Identity.Name;

                var replyId = await _commentService.AddCommentAsync(new CommentCreateModel
                {
                    ArticleId = parentComment.ArticleId,
                    UserId = userId,
                    UserName = userName,
                    Content = model.Content,
                    ParentCommentId = id,
                    CreatedDate = DateTime.UtcNow
                });

                var reply = await _commentService.GetCommentAsync(replyId);

                return Ok(new
                {
                    success = true,
                    reply = new
                    {
                        id = reply.Id,
                        content = reply.Content,
                        userName = reply.UserName,
                        createdDate = reply.CreatedDate,
                        parentCommentId = reply.ParentCommentId
                    },
                    message = "Reply added successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while adding the reply" });
            }
        }

        // POST: /api/comments/{id}/like
        [HttpPost]
        [Route("api/comments/{id}/like")]
        [Authorize]
        public async Task<IActionResult> LikeComment(string id)
        {
            try
            {
                var comment = await _commentService.GetCommentAsync(id);
                if (comment == null)
                {
                    return NotFound(new { success = false, message = "Comment not found" });
                }

                var userId = User.Identity.Name;
                var isLiked = await _commentService.IsCommentLikedByUserAsync(id, userId);

                if (isLiked)
                {
                    await _commentService.UnlikeCommentAsync(id, userId);
                    var unlikedCount = await _commentService.GetCommentLikesCountAsync(id);
                    return Ok(new { success = true, liked = false, likesCount = unlikedCount });
                }
                else
                {
                    await _commentService.LikeCommentAsync(id, userId);
                    var likedCount = await _commentService.GetCommentLikesCountAsync(id);
                    return Ok(new { success = true, liked = true, likesCount = likedCount });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while liking the comment" });
            }
        }

        // POST: /api/articles/{id}/rate
        [HttpPost]
        [Route("api/articles/{id}/rate")]
        [Authorize]
        public async Task<IActionResult> RateArticle(string id, [FromBody] RateArticleModel model)
        {
            if (model.Rating < 1 || model.Rating > 5)
            {
                return BadRequest(new { success = false, message = "Rating must be between 1 and 5" });
            }

            try
            {
                var article = await _contentManager.GetAsync(id);
                if (article == null || !article.Published || article.ContentType != "NewsArticle")
                {
                    return NotFound(new { success = false, message = "Article not found" });
                }

                var userId = User.Identity.Name;

                await _articleRatingService.RateArticleAsync(new ArticleRatingModel
                {
                    ArticleId = id,
                    UserId = userId,
                    Rating = model.Rating,
                    RatedDate = DateTime.UtcNow
                });

                var updatedRating = await _articleRatingService.GetArticleRatingAsync(id);

                return Ok(new
                {
                    success = true,
                    averageRating = updatedRating.AverageRating,
                    totalRatings = updatedRating.TotalRatings,
                    userRating = model.Rating,
                    message = "Article rated successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while rating the article" });
            }
        }

        // POST: /api/newsletter/subscribe
        [HttpPost]
        [Route("api/newsletter/subscribe")]
        public async Task<IActionResult> SubscribeNewsletter([FromBody] NewsletterSubscriptionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid email address" });
            }

            try
            {
                // Here you would implement newsletter subscription logic
                // For now, we'll just return success
                return Ok(new { success = true, message = "Successfully subscribed to newsletter" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "An error occurred while subscribing to newsletter" });
            }
        }

        // Helper method to get related articles
        private async Task<List<dynamic>> GetRelatedArticlesAsync(ContentItem article)
        {
            var category = article.Content.NewsArticle.Category?.Text;
            var tags = article.Content.NewsArticle.Tags?.Text?.Split(',');

            var query = _session.Query<ContentItem>()
                .Where(x => x.ContentType == "NewsArticle" && 
                           x.Published && 
                           x.ContentItemId != article.ContentItemId);

            // Prefer same category
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Content.NewsArticle.Category.Text == category);
            }

            var relatedArticles = await query.Take(3).ListAsync();
            var shapes = new List<dynamic>();

            foreach (var relatedArticle in relatedArticles)
            {
                var shape = await _contentItemDisplayManager.BuildDisplayAsync(relatedArticle, _updateModelAccessor.ModelUpdater, "Summary");
                shapes.Add(shape);
            }

            return shapes;
        }
    }

    // API Models
    public class AddCommentModel
    {
        public string ArticleId { get; set; }
        public string Content { get; set; }
        public string ParentCommentId { get; set; }
    }

    public class ReplyCommentModel
    {
        public string Content { get; set; }
    }

    public class RateArticleModel
    {
        public int Rating { get; set; }
    }

    public class NewsletterSubscriptionModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}