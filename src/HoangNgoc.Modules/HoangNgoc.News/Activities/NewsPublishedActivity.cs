using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.ContentManagement;
using HoangNgoc.News.Models;
using Microsoft.Extensions.Localization;

namespace HoangNgoc.News.Activities
{
    public class NewsPublishedActivity : EventActivity
    {
        public static string EventName => nameof(NewsPublishedActivity);
        
        private readonly IStringLocalizer S;

        public NewsPublishedActivity(IStringLocalizer<NewsPublishedActivity> localizer)
        {
            S = localizer;
        }

        public override string Name => EventName;
        public override LocalizedString DisplayText => S["News Published"];
        public override LocalizedString Category => S["News"];

        public override bool CanExecute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            return contentItem?.ContentType == "NewsArticle" && contentItem.Published;
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            var newsArticlePart = contentItem?.As<NewsArticlePart>();

            if (newsArticlePart != null)
            {
                // Add news-specific data to workflow context
                workflowContext.Properties["NewsTitle"] = contentItem.DisplayText;
                workflowContext.Properties["NewsAuthor"] = contentItem.Author;
                workflowContext.Properties["NewsPublishedDate"] = contentItem.PublishedUtc;
                workflowContext.Properties["NewsIsFeatured"] = newsArticlePart.IsFeatured?.Value ?? false;
            }

            return Outcomes("Done");
        }
    }
}