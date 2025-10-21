using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using YesSql.Indexes;
using HoangNgoc.Comment.Drivers;
using HoangNgoc.Comment.Indexes;
using HoangNgoc.Comment.Models;
using HoangNgoc.Comment.Services;

namespace HoangNgoc.Comment
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register ContentPart
            services.AddContentPart<CommentPart>();

            // Register Display Driver
            services.AddScoped<IContentPartDisplayDriver, CommentPartDisplayDriver>();

            // Register Services
            services.AddScoped<ICommentService, CommentService>();

            // Register Navigation
            services.AddScoped<INavigationProvider, AdminMenu>();

            // Register Migrations
            services.AddScoped<IDataMigration, Migrations.CommentMigrations>();

            // Register Indexes
            // TODO: Enable after implementing CommentPart properly
            // services.AddSingleton<IIndexProvider, CommentIndexProvider>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Public routes for CommentController
            routes.MapAreaControllerRoute(
                name: "HoangNgoc.Comment.Comment",
                areaName: "HoangNgoc.Comment",
                pattern: "Comment/{action=Index}/{id?}",
                defaults: new { controller = "Comment" }
            );

            // Admin controllers use OrchardCore convention-based routing via [Admin] attributes
        }
    }
}