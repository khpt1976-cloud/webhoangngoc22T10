using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using YesSql.Indexes;
using HoangNgoc.Payment.Drivers;
using HoangNgoc.Payment.Indexes;
using HoangNgoc.Payment.Models;
using HoangNgoc.Payment.Services;
using HoangNgoc.Payment.AdminMenu;
using HoangNgoc.Payment.Migrations;

namespace HoangNgoc.Payment
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register Content Parts
            services.AddContentPart<PaymentPart>();
            services.AddContentPart<InvoicePart>();
            services.AddContentPart<OrderPart>();

            // Register Display Drivers
            services.AddScoped<IContentPartDisplayDriver, PaymentPartDisplayDriver>();

            // Register Services
            services.AddScoped<IPaymentService, PaymentService>();

            // Register Indexes
            services.AddSingleton<IIndexProvider, PaymentIndexProvider>();

            // Register Migrations
            services.AddScoped<IDataMigration, PaymentMigrations>();

            // Register Admin Menu
            services.AddScoped<INavigationProvider, PaymentAdminMenu>();
        }
    }
}