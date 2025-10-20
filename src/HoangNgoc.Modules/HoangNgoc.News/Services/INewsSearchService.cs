using OrchardCore.ContentManagement;

namespace HoangNgoc.News.Services
{
    public interface INewsSearchService
    {
        Task<IEnumerable<ContentItem>> SearchNewsAsync(string query, int page = 1, int pageSize = 10);
        Task<int> GetSearchResultCountAsync(string query);
        Task<IEnumerable<ContentItem>> SearchNewsByTagsAsync(string[] tags, int page = 1, int pageSize = 10);
        Task<IEnumerable<ContentItem>> SearchNewsByCategoryAsync(string categoryId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ContentItem>> GetRelatedNewsAsync(ContentItem newsArticle, int count = 5);
        Task<IEnumerable<string>> GetPopularTagsAsync(int count = 20);
        Task<IEnumerable<ContentItem>> GetNewsByDateRangeAsync(DateTime startDate, DateTime endDate, int page = 1, int pageSize = 10);
    }
}