using OrchardCore.Data.Migration;

namespace HoangNgoc.Payment.Migrations
{
    public class PaymentMigrations : DataMigration
    {
        public PaymentMigrations()
        {
        }

        public async Task<int> CreateAsync()
        {
            // Temporarily disabled - requires OrchardCore.ContentManagement module
            return 1;
        }
    }
}
