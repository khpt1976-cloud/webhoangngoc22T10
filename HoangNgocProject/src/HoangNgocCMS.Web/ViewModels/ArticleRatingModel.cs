namespace HoangNgocCMS.Web.ViewModels
{
    public class ArticleRatingModel
    {
        public string Id { get; set; } = string.Empty;
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsVerified { get; set; }
        public string? ReviewTitle { get; set; }
        public string? ReviewContent { get; set; }
        public int HelpfulVotes { get; set; }
        public int UnhelpfulVotes { get; set; }
        public bool IsRecommended { get; set; }
        public string? UserAvatar { get; set; }
        public string? UserLocation { get; set; }
        public bool IsAnonymous { get; set; }
        public string Status { get; set; } = "Active";
    }

    public class ArticleRatingCreateModel
    {
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? ReviewTitle { get; set; }
        public string? ReviewContent { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAnonymous { get; set; }
    }

    public class ArticleRatingUpdateModel
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? ReviewTitle { get; set; }
        public string? ReviewContent { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsAnonymous { get; set; }
    }
}