using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using HoangNgoc.News.Indexes;
using HoangNgoc.News.Models;
using OrchardCore.Taxonomies.Fields;

namespace HoangNgoc.News.Services
{
    public class NewsSearchService : INewsSearchService
    {
        private readonly YesSql.ISession _session;

        public NewsSearchService(YesSql.ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<ContentItem>> SearchNewsAsync(string query, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Enumerable.Empty<ContentItem>();

            var searchTerms = query.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published)
                .Where(x => searchTerms.Any(term => 
                    x.Title.ToLowerInvariant().Contains(term) ||
                    x.Summary.ToLowerInvariant().Contains(term) ||
                    x.Content.ToLowerInvariant().Contains(term)))
                .OrderByDescending(x => x.PublishedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ListAsync();
        }

        public async Task<int> GetSearchResultCountAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return 0;

            var searchTerms = query.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published)
                .Where(x => searchTerms.Any(term => 
                    x.Title.ToLowerInvariant().Contains(term) ||
                    x.Summary.ToLowerInvariant().Contains(term) ||
                    x.Content.ToLowerInvariant().Contains(term)))
                .CountAsync();
        }

        public async Task<IEnumerable<ContentItem>> SearchNewsByTagsAsync(string[] tags, int page = 1, int pageSize = 10)
        {
            if (tags == null || tags.Length == 0)
                return Enumerable.Empty<ContentItem>();

            return await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published)
                .Where(x => tags.Any(tag => x.Tags.Contains(tag)))
                .OrderByDescending(x => x.PublishedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> SearchNewsByCategoryAsync(string categoryId, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return Enumerable.Empty<ContentItem>();

            return await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published && x.Category == categoryId)
                .OrderByDescending(x => x.PublishedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetRelatedNewsAsync(ContentItem newsArticle, int count = 5)
        {
            var newsArticlePart = newsArticle.As<NewsArticlePart>();
            if (newsArticlePart == null)
                return Enumerable.Empty<ContentItem>();

            // Get fields from ContentItem
            var categoryField = newsArticle.Content.NewsArticlePart?.Category as OrchardCore.Taxonomies.Fields.TaxonomyField;
            var tagsField = newsArticle.Content.NewsArticlePart?.Tags as OrchardCore.Taxonomies.Fields.TaxonomyField;

            var categoryId = categoryField?.TermContentItemIds?.FirstOrDefault();
            var tags = tagsField?.TermContentItemIds ?? new string[0];

            var query = _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published && x.ContentItemId != newsArticle.ContentItemId);

            // Prioritize by category first, then by tags
            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.Category == categoryId);
            }
            else if (tags.Any())
            {
                query = query.Where(x => tags.Any(tag => x.Tags.Contains(tag)));
            }

            return await query
                .OrderByDescending(x => x.PublishedUtc)
                .Take(count)
                .ListAsync();
        }

        public async Task<IEnumerable<string>> GetPopularTagsAsync(int count = 20)
        {
            var newsIndexes = await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(index => index.Published)
                .ListAsync();

            var tagCounts = new Dictionary<string, int>();

            foreach (var contentItem in newsIndexes)
            {
                var tagsField = contentItem.Content.NewsArticlePart?.Tags as TaxonomyField;
                if (tagsField?.TermContentItemIds != null)
                {
                    foreach (var tagId in tagsField.TermContentItemIds)
                    {
                        if (tagCounts.ContainsKey(tagId))
                            tagCounts[tagId]++;
                        else
                            tagCounts[tagId] = 1;
                    }
                }
            }

            return tagCounts
                .OrderByDescending(x => x.Value)
                .Take(count)
                .Select(x => x.Key);
        }

        public async Task<IEnumerable<ContentItem>> GetNewsByDateRangeAsync(DateTime startDate, DateTime endDate, int page = 1, int pageSize = 10)
        {
            return await _session.Query<ContentItem, NewsArticleIndex>()
                .Where(x => x.Published)
                .Where(x => x.PublishedUtc >= startDate && x.PublishedUtc <= endDate)
                .OrderByDescending(x => x.PublishedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ListAsync();
        }
    }
}