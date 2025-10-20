using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using HoangNgoc.Authentication.Migrations;

namespace HoangNgoc.Authentication;

public class Startup : OrchardCore.Modules.StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Register Migrations
        services.AddScoped<IDataMigration, AuthenticationMigrations>();
        
        // OrchardCore.Users đã cung cấp tất cả authentication services cần thiết
        // Chúng ta chỉ cần đăng ký routes
    }
}