using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.ContentManagement;
using HoangNgoc.News.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HoangNgoc.News.Activities
{
    public class SendNewsNotificationActivity : TaskActivity
    {
        private readonly ILogger<SendNewsNotificationActivity> _logger;
        private readonly IStringLocalizer S;

        public SendNewsNotificationActivity(
            ILogger<SendNewsNotificationActivity> logger,
            IStringLocalizer<SendNewsNotificationActivity> localizer)
        {
            _logger = logger;
            S = localizer;
        }

        public override string Name => nameof(SendNewsNotificationActivity);
        public override LocalizedString DisplayText => S["Send News Notification"];
        public override LocalizedString Category => S["News"];

        public string NotificationType
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public string Recipients
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public override bool CanExecute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            return contentItem?.ContentType == "NewsArticle";
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var contentItem = workflowContext.Input["ContentItem"] as ContentItem;
            if (contentItem?.ContentType != "NewsArticle")
                return Outcomes("Failed");

            var newsArticlePart = contentItem.As<NewsArticlePart>();
            if (newsArticlePart == null)
                return Outcomes("Failed");

            try
            {
                // Log notification (in real implementation, this would send actual notifications)
                var notificationType = NotificationType ?? "published";
                var recipients = Recipients ?? "all";

                _logger.LogInformation(
                    "News notification sent: Type={NotificationType}, Recipients={Recipients}, NewsTitle={NewsTitle}, Author={Author}",
                    notificationType,
                    recipients,
                    contentItem.DisplayText,
                    contentItem.Author
                );

                // Add notification info to workflow context
                workflowContext.Properties["NotificationSent"] = true;
                workflowContext.Properties["NotificationType"] = notificationType;
                workflowContext.Properties["NotificationRecipients"] = recipients;

                return Outcomes("Done");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send news notification for {NewsTitle}", contentItem.DisplayText);
                workflowContext.Properties["NotificationError"] = ex.Message;
                return Outcomes("Failed");
            }
        }
    }
}