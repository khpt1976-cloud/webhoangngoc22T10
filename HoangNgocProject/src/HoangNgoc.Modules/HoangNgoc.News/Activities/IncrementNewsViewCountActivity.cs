using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.ContentManagement;
using HoangNgoc.News.Models;
using Microsoft.Extensions.Localization;

namespace HoangNgoc.News.Activities
{
    public class IncrementNewsViewCountActivity : TaskActivity
    {
        private readonly IContentManager _contentManager;
        private readonly IStringLocalizer S;

        public IncrementNewsViewCountActivity(
            IContentManager contentManager,
            IStringLocalizer<IncrementNewsViewCountActivity> localizer)
        {
            _contentManager = contentManager;
            S = localizer;
        }

        public override string Name => nameof(IncrementNewsViewCountActivity);
        public override LocalizedString DisplayText => S["Increment News View Count"];
        public override LocalizedString Category => S["News"];

        public override bool CanExecute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            return contentItem?.ContentType == "NewsArticle";
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            if (contentItem?.ContentType != "NewsArticle")
                return Outcomes("Done");

            var newsArticlePart = contentItem.As<NewsArticlePart>();
            if (newsArticlePart != null)
            {
                // Increment view count
                var currentCount = newsArticlePart.ViewCount?.Value ?? 0;
                newsArticlePart.ViewCount.Value = currentCount + 1;

                // Update the content item
                await _contentManager.UpdateAsync(contentItem);

                // Add updated count to workflow context
                workflowContext.Properties["NewsViewCount"] = newsArticlePart.ViewCount.Value;
            }

            return Outcomes("Done");
        }
    }
}