using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
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

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        // Public routes for TestController
        routes.MapAreaControllerRoute(
            name: "HoangNgoc.Authentication.Test",
            areaName: "HoangNgoc.Authentication",
            pattern: "Auth/{action=Index}/{id?}",
            defaults: new { controller = "Test" }
        );
    }
}