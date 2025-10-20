using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Queries;
using OrchardCore.Settings;
using HoangNgoc.News.Models;
using HoangNgoc.News.Services;

namespace HoangNgoc.News.Controllers;

public class NewsController : Controller
{
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IShapeFactory _shapeFactory;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly ISiteService _siteService;
    private readonly INewsService _newsService;
    private readonly INewsSearchService _newsSearchService;

    public NewsController(
        IContentManager contentManager,
        IContentItemDisplayManager contentItemDisplayManager,
        IShapeFactory shapeFactory,
        IUpdateModelAccessor updateModelAccessor,
        ISiteService siteService,
        INewsService newsService,
        INewsSearchService newsSearchService)
    {
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _shapeFactory = shapeFactory;
        _updateModelAccessor = updateModelAccessor;
        _siteService = siteService;
        _newsService = newsService;
        _newsSearchService = newsSearchService;
    }

    [Route("news")]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var newsArticles = await _newsService.GetPublishedNewsAsync(page, pageSize);
        var totalCount = await _newsService.GetPublishedNewsCountAsync();

        var shape = await _shapeFactory.CreateAsync("NewsList", Arguments.From(new
        {
            NewsArticles = newsArticles,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        }));

        return View(shape);
    }

    [Route("news/{slug}")]
    public async Task<IActionResult> Details(string slug)
    {
        var newsArticle = await _newsService.GetNewsBySlugAsync(slug);
        if (newsArticle == null)
        {
            return NotFound();
        }

        // Increment view count
        await _newsService.IncrementViewCountAsync(newsArticle);

        var shape = await _contentItemDisplayManager.BuildDisplayAsync(newsArticle, _updateModelAccessor.ModelUpdater, "Detail");
        return View(shape);
    }

    [Route("news/category/{categorySlug}")]
    public async Task<IActionResult> Category(string categorySlug, int page = 1, int pageSize = 10)
    {
        var newsArticles = await _newsService.GetNewsByCategoryAsync(categorySlug, page, pageSize);
        var totalCount = await _newsService.GetNewsByCategoryCountAsync(categorySlug);

        var shape = await _shapeFactory.CreateAsync("NewsList", Arguments.From(new
        {
            NewsArticles = newsArticles,
            CategorySlug = categorySlug,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        }));

        return View("Index", shape);
    }

    [Route("news/featured")]
    public async Task<IActionResult> Featured(int page = 1, int pageSize = 10)
    {
        var featuredNews = await _newsService.GetFeaturedNewsAsync(page, pageSize);
        var totalCount = await _newsService.GetFeaturedNewsCountAsync();

        var shape = await _shapeFactory.CreateAsync("NewsList", Arguments.From(new
        {
            NewsArticles = featuredNews,
            IsFeatured = true,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        }));

        return View("Index", shape);
    }

    [Route("news/search")]
    public async Task<IActionResult> Search(string q, int page = 1, int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return RedirectToAction(nameof(Index));
        }

        var searchResults = await _newsSearchService.SearchNewsAsync(q, page, pageSize);
        var totalCount = await _newsSearchService.GetSearchResultCountAsync(q);

        var shape = await _shapeFactory.CreateAsync("NewsList", Arguments.From(new
        {
            NewsArticles = searchResults,
            SearchQuery = q,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        }));

        return View("Index", shape);
    }
}