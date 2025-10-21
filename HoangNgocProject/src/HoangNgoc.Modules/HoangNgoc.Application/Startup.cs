using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using YesSql.Indexes;
using HoangNgoc.Application.Drivers;
using HoangNgoc.Application.Indexes;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.Services;
using HoangNgoc.Application.Migrations;

namespace HoangNgoc.Application
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register Content Parts
            // TODO: Enable after creating parts and display drivers
            // services.AddContentPart<JobApplicationPart>();
            // services.AddContentPart<JobPostingPart>();
            // services.AddContentPart<CandidatePart>();

            // Register Display Drivers
            // TODO: Enable after creating display drivers
            // services.AddScoped<IContentPartDisplayDriver, JobApplicationPartDisplayDriver>();
            // services.AddScoped<IContentPartDisplayDriver, JobPostingPartDisplayDriver>();
            // services.AddScoped<IContentPartDisplayDriver, CandidatePartDisplayDriver>();

            // Register Services
            services.AddScoped<IApplicationService, ApplicationService>();

            // Register Indexes
            // TODO: Enable after creating content types and parts
            // services.AddSingleton<IIndexProvider, JobApplicationIndexProvider>();
            // services.AddSingleton<IIndexProvider, JobPostingIndexProvider>();
            // services.AddSingleton<IIndexProvider, CandidateIndexProvider>();

            // Register Migrations
            services.AddDataMigration<ApplicationMigrations>();

            // Register Navigation
            services.AddScoped<INavigationProvider, AdminMenu>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Public routes for JobController (not admin)
            routes.MapAreaControllerRoute(
                name: "HoangNgoc.Application.Job",
                areaName: "HoangNgoc.Application",
                pattern: "Job/{action=Index}/{id?}",
                defaults: new { controller = "Job" }
            );

            // Public routes for ApplicationController
            routes.MapAreaControllerRoute(
                name: "HoangNgoc.Application.Application",
                areaName: "HoangNgoc.Application",
                pattern: "Application/{action=Index}/{id?}",
                defaults: new { controller = "Application" }
            );

            // Public routes for CandidateController
            routes.MapAreaControllerRoute(
                name: "HoangNgoc.Application.Candidate",
                areaName: "HoangNgoc.Application",
                pattern: "Candidate/{action=Index}/{id?}",
                defaults: new { controller = "Candidate" }
            );

            // Admin controllers use OrchardCore convention-based routing via [Admin] attributes
        }
    }
}