using OrchardCore.Data.Migration;

namespace HoangNgoc.Training.Migrations
{
    public class TrainingMigrations : DataMigration
    {
        public TrainingMigrations()
        {
        }

        public async Task<int> CreateAsync()
        {
            // Temporarily disabled - requires OrchardCore.ContentManagement module
            return 1;
        }
    }
}
