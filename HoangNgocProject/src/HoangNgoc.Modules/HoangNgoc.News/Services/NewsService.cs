using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Autoroute.Models;
using OrchardCore.Taxonomies.Models;
using YesSql;
using HoangNgoc.News.Models;

namespace HoangNgoc.News.Services;

public class NewsService : INewsService
{
    private readonly IContentManager _contentManager;
    private readonly YesSql.ISession _session;

    public NewsService(IContentManager contentManager, YesSql.ISession session)
    {
        _contentManager = contentManager;
        _session = session;
    }

    public async Task<IEnumerable<ContentItem>> GetPublishedNewsAsync(int page = 1, int pageSize = 10)
    {
        return await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ListAsync();
    }

    public async Task<int> GetPublishedNewsCountAsync()
    {
        return await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .CountAsync();
    }

    public async Task<IEnumerable<ContentItem>> GetFeaturedNewsAsync(int page = 1, int pageSize = 10)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .ListAsync();

        var featuredNews = new List<ContentItem>();
        
        foreach (var item in contentItems)
        {
            var newsArticlePart = item.As<NewsArticlePart>();
            if (newsArticlePart?.IsFeatured?.Value == true)
            {
                featuredNews.Add(item);
            }
        }

        return featuredNews
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> GetFeaturedNewsCountAsync()
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems.Count(item => item.As<NewsArticlePart>()?.IsFeatured?.Value == true);
    }

    public async Task<IEnumerable<ContentItem>> GetNewsByCategoryAsync(string categorySlug, int page = 1, int pageSize = 10)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .ListAsync();

        var categoryNews = new List<ContentItem>();
        
        foreach (var item in contentItems)
        {
            var taxonomyPart = item.As<TaxonomyPart>();
            if (taxonomyPart?.Terms?.Any(term => 
                term.As<AutoroutePart>()?.Path?.Contains(categorySlug) == true) == true)
            {
                categoryNews.Add(item);
            }
        }

        return categoryNews
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> GetNewsByCategoryCountAsync(string categorySlug)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems.Count(item =>
        {
            var taxonomyPart = item.As<TaxonomyPart>();
            return taxonomyPart?.Terms?.Any(term => 
                term.As<AutoroutePart>()?.Path?.Contains(categorySlug) == true) == true;
        });
    }

    public async Task<ContentItem?> GetNewsBySlugAsync(string slug)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems.FirstOrDefault(item =>
            item.As<AutoroutePart>()?.Path?.EndsWith(slug) == true);
    }

    public async Task<ContentItem?> GetNewsByIdAsync(string contentItemId)
    {
        return await _contentManager.GetAsync(contentItemId, VersionOptions.Published);
    }

    public async Task<IEnumerable<ContentItem>> SearchNewsAsync(string query, int page = 1, int pageSize = 10)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .ListAsync();

        var searchResults = contentItems.Where(item =>
            item.DisplayText.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            item.As<NewsArticlePart>()?.Summary?.Text?.Contains(query, StringComparison.OrdinalIgnoreCase) == true ||
            item.As<NewsArticlePart>()?.Author?.Text?.Contains(query, StringComparison.OrdinalIgnoreCase) == true);

        return searchResults
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> SearchNewsCountAsync(string query)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems.Count(item =>
            item.DisplayText.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            item.As<NewsArticlePart>()?.Summary?.Text?.Contains(query, StringComparison.OrdinalIgnoreCase) == true ||
            item.As<NewsArticlePart>()?.Author?.Text?.Contains(query, StringComparison.OrdinalIgnoreCase) == true);
    }

    public async Task<IEnumerable<ContentItem>> GetLatestNewsAsync(int count = 5)
    {
        return await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .Take(count)
            .ListAsync();
    }

    public async Task<IEnumerable<ContentItem>> GetRelatedNewsAsync(ContentItem newsArticle, int count = 5)
    {
        var taxonomyPart = newsArticle.As<TaxonomyPart>();
        if (taxonomyPart?.Terms == null || !taxonomyPart.Terms.Any())
        {
            return await GetLatestNewsAsync(count);
        }

        var allNews = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published && x.ContentItemId != newsArticle.ContentItemId)
            .OrderByDescending(x => x.CreatedUtc)
            .ListAsync();

        var relatedNews = allNews.Where(item =>
        {
            var itemTaxonomyPart = item.As<TaxonomyPart>();
            return itemTaxonomyPart?.Terms?.Any(term =>
                taxonomyPart.Terms.Any(originalTerm => originalTerm.ContentItemId == term.ContentItemId)) == true;
        });

        return relatedNews.Take(count);
    }

    public async Task IncrementViewCountAsync(ContentItem newsArticle)
    {
        var newsArticlePart = newsArticle.As<NewsArticlePart>();
        if (newsArticlePart?.ViewCount != null)
        {
            var currentCount = newsArticlePart.ViewCount.Value ?? 0;
            newsArticlePart.ViewCount.Value = currentCount + 1;
            
            await _contentManager.UpdateAsync(newsArticle);
        }
    }

    public async Task<IEnumerable<ContentItem>> GetMostViewedNewsAsync(int count = 10)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems
            .OrderByDescending(item => item.As<NewsArticlePart>()?.ViewCount?.Value ?? 0)
            .Take(count);
    }

    public async Task<IEnumerable<ContentItem>> GetNewsByAuthorAsync(string author, int page = 1, int pageSize = 10)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .OrderByDescending(x => x.CreatedUtc)
            .ListAsync();

        var authorNews = contentItems.Where(item =>
            item.As<NewsArticlePart>()?.Author?.Text?.Equals(author, StringComparison.OrdinalIgnoreCase) == true);

        return authorNews
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<int> GetNewsByAuthorCountAsync(string author)
    {
        var contentItems = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentType == "NewsArticle" && x.Published)
            .ListAsync();

        return contentItems.Count(item =>
            item.As<NewsArticlePart>()?.Author?.Text?.Equals(author, StringComparison.OrdinalIgnoreCase) == true);
    }
}