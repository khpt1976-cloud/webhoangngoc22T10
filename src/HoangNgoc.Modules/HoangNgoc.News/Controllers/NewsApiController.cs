using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using HoangNgoc.News.Services;
using HoangNgoc.News.Models;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.News.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly INewsSearchService _newsSearchService;

        public NewsApiController(INewsService newsService, INewsSearchService newsSearchService)
        {
            _newsService = newsService;
            _newsSearchService = newsSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews(int page = 1, int pageSize = 10)
        {
            var news = await _newsService.GetPublishedNewsAsync(page, pageSize);
            var totalCount = await _newsService.GetPublishedNewsCountAsync();

            return Ok(new
            {
                data = news.Select(MapToApiModel),
                pagination = new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(string id)
        {
            var newsArticle = await _newsService.GetNewsByIdAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            // Increment view count
            await _newsService.IncrementViewCountAsync(newsArticle);

            return Ok(MapToApiModel(newsArticle));
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedNews(int count = 5)
        {
            var featuredNews = await _newsService.GetFeaturedNewsAsync(count);
            return Ok(featuredNews.Select(MapToApiModel));
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestNews(int count = 10)
        {
            var latestNews = await _newsService.GetLatestNewsAsync(count);
            return Ok(latestNews.Select(MapToApiModel));
        }

        [HttpGet("most-viewed")]
        public async Task<IActionResult> GetMostViewedNews(int count = 10)
        {
            var mostViewedNews = await _newsService.GetMostViewedNewsAsync(count);
            return Ok(mostViewedNews.Select(MapToApiModel));
        }

        [HttpGet("category/{categorySlug}")]
        public async Task<IActionResult> GetNewsByCategory(string categorySlug, int page = 1, int pageSize = 10)
        {
            var news = await _newsService.GetNewsByCategoryAsync(categorySlug, page, pageSize);
            var totalCount = await _newsService.GetNewsByCategoryCountAsync(categorySlug);

            return Ok(new
            {
                data = news.Select(MapToApiModel),
                pagination = new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchNews(string q, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Search query is required");
            }

            var searchResults = await _newsSearchService.SearchNewsAsync(q, page, pageSize);
            var totalCount = await _newsSearchService.GetSearchResultCountAsync(q);

            return Ok(new
            {
                data = searchResults.Select(MapToApiModel),
                query = q,
                pagination = new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetPopularTags(int count = 20)
        {
            var tags = await _newsSearchService.GetPopularTagsAsync(count);
            return Ok(tags);
        }

        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedNews(string id, int count = 5)
        {
            var newsArticle = await _newsService.GetNewsByIdAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            var relatedNews = await _newsSearchService.GetRelatedNewsAsync(newsArticle, count);
            return Ok(relatedNews.Select(MapToApiModel));
        }

        private object MapToApiModel(ContentItem contentItem)
        {
            var newsArticlePart = contentItem.As<NewsArticlePart>();
            
            // Get fields from ContentItem
            var titleField = contentItem.Content.NewsArticlePart?.Title as TextField;
            var contentField = contentItem.Content.NewsArticlePart?.Content as HtmlField;
            
            return new
            {
                id = contentItem.ContentItemId,
                title = titleField?.Text,
                summary = newsArticlePart?.Summary?.Text,
                content = contentField?.Html,
                isFeatured = newsArticlePart?.IsFeatured?.Value ?? false,
                viewCount = (int)(newsArticlePart?.ViewCount?.Value ?? 0),
                publishedUtc = contentItem.PublishedUtc,
                createdUtc = contentItem.CreatedUtc,
                modifiedUtc = contentItem.ModifiedUtc,
                author = contentItem.Author,
                slug = contentItem.DisplayText?.Replace(" ", "-").ToLowerInvariant()
            };
        }
    }
}