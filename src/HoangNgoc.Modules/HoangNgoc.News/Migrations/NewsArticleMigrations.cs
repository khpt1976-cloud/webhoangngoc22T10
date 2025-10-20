using OrchardCore.Data.Migration;

namespace HoangNgoc.News.Migrations
{
    public class NewsArticleMigrations : DataMigration
    {
        public NewsArticleMigrations()
        {
        }

        public async Task<int> CreateAsync()
        {
            // Temporarily disabled - requires OrchardCore.ContentManagement module
            return 1;
        }
    }
}
