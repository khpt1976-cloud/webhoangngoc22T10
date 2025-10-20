namespace HoangNgocCMS.Web.Models
{
    public class ArticleRating
    {
        public string Id { get; set; } = string.Empty;
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Review { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Additional properties for aggregated data
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }
}