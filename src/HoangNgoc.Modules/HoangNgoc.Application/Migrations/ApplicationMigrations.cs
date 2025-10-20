using OrchardCore.Data.Migration;

namespace HoangNgoc.Application.Migrations
{
    public class ApplicationMigrations : DataMigration
    {
        public ApplicationMigrations()
        {
        }

        public async Task<int> CreateAsync()
        {
            // Temporarily disabled - requires OrchardCore.ContentManagement module to be properly enabled
            // This migration creates content types for JobApplication, JobPosting, and Candidate
            // Will be re-enabled once OrchardCore.ContentManagement dependency is resolved
            return 1;
        }

        public async Task<int> UpdateFrom1()
        {
            // Temporarily disabled
            return 2;
        }

        public async Task<int> UpdateFrom2()
        {
            // Temporarily disabled
            return 3;
        }

        public async Task<int> UpdateFrom3()
        {
            // Temporarily disabled
            return 4;
        }
    }
}