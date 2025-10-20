using OrchardCore.ContentManagement;

namespace HoangNgoc.News.Services;

public interface INewsService
{
    Task<IEnumerable<ContentItem>> GetPublishedNewsAsync(int page = 1, int pageSize = 10);
    Task<int> GetPublishedNewsCountAsync();
    
    Task<IEnumerable<ContentItem>> GetFeaturedNewsAsync(int page = 1, int pageSize = 10);
    Task<int> GetFeaturedNewsCountAsync();
    
    Task<IEnumerable<ContentItem>> GetNewsByCategoryAsync(string categorySlug, int page = 1, int pageSize = 10);
    Task<int> GetNewsByCategoryCountAsync(string categorySlug);
    
    Task<ContentItem?> GetNewsBySlugAsync(string slug);
    Task<ContentItem?> GetNewsByIdAsync(string contentItemId);
    
    Task<IEnumerable<ContentItem>> SearchNewsAsync(string query, int page = 1, int pageSize = 10);
    Task<int> SearchNewsCountAsync(string query);
    
    Task<IEnumerable<ContentItem>> GetLatestNewsAsync(int count = 5);
    Task<IEnumerable<ContentItem>> GetRelatedNewsAsync(ContentItem newsArticle, int count = 5);
    
    Task IncrementViewCountAsync(ContentItem newsArticle);
    Task<IEnumerable<ContentItem>> GetMostViewedNewsAsync(int count = 10);
    
    Task<IEnumerable<ContentItem>> GetNewsByAuthorAsync(string author, int page = 1, int pageSize = 10);
    Task<int> GetNewsByAuthorCountAsync(string author);
}