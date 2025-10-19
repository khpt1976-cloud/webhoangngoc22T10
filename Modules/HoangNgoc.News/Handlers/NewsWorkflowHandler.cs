using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.Workflows.Services;
using HoangNgoc.News.Models;
using HoangNgoc.News.Activities;

namespace HoangNgoc.News.Handlers
{
    public class NewsWorkflowHandler : ContentHandlerBase
    {
        private readonly IWorkflowManager _workflowManager;

        public NewsWorkflowHandler(IWorkflowManager workflowManager)
        {
            _workflowManager = workflowManager;
        }

        public override async Task PublishedAsync(PublishContentContext context)
        {
            if (context.ContentItem.ContentType == "NewsArticle")
            {
                var newsArticlePart = context.ContentItem.As<NewsArticlePart>();
                if (newsArticlePart != null)
                {
                    // Trigger News Published workflow
                    await _workflowManager.TriggerEventAsync(
                        NewsPublishedActivity.EventName,
                        input: new { ContentItem = context.ContentItem },
                        correlationId: context.ContentItem.ContentItemId
                    );
                }
            }
        }

        public override async Task UnpublishedAsync(PublishContentContext context)
        {
            if (context.ContentItem.ContentType == "NewsArticle")
            {
                var newsArticlePart = context.ContentItem.As<NewsArticlePart>();
                if (newsArticlePart != null)
                {
                    // Trigger News Unpublished workflow
                    await _workflowManager.TriggerEventAsync(
                        NewsUnpublishedActivity.EventName,
                        input: new { ContentItem = context.ContentItem },
                        correlationId: context.ContentItem.ContentItemId
                    );
                }
            }
        }

        public override async Task UpdatedAsync(UpdateContentContext context)
        {
            if (context.ContentItem.ContentType == "NewsArticle")
            {
                var newsArticlePart = context.ContentItem.As<NewsArticlePart>();
                if (newsArticlePart != null)
                {
                    // You can add custom workflow triggers here for updates
                    // For example, when featured status changes, etc.
                }
            }
        }
    }
}