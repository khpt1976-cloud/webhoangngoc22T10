using OrchardCore.Data.Migration;

namespace HoangNgoc.Comment.Migrations
{
    public class CommentMigrations : DataMigration
    {
        public CommentMigrations()
        {
        }

        public async Task<int> CreateAsync()
        {
            // Temporarily disabled - requires OrchardCore.ContentManagement module
            return 1;
        }
    }
}
