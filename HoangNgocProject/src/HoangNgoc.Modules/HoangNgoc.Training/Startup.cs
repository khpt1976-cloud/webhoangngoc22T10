using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using YesSql.Indexes;
using HoangNgoc.Training.Models;
using HoangNgoc.Training.Drivers;
using HoangNgoc.Training.Migrations;
using HoangNgoc.Training.Services;
using HoangNgoc.Training.Indexes;
using HoangNgoc.Training.AdminMenu;

namespace HoangNgoc.Training
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register Content Parts
            services.AddContentPart<CoursePart>();
            services.AddContentPart<LessonPart>();
            services.AddContentPart<EnrollmentPart>();

            // Register Display Drivers
            services.AddScoped<IContentPartDisplayDriver, CoursePartDisplayDriver>();
            services.AddScoped<IContentPartDisplayDriver, LessonPartDisplayDriver>();
            services.AddScoped<IContentPartDisplayDriver, EnrollmentPartDisplayDriver>();

            // Register Migrations
            services.AddScoped<IDataMigration, TrainingMigrations>();

            // Register Services
            services.AddScoped<ITrainingService, TrainingService>();

            // Register Indexes
            // TODO: Enable after implementing Parts properly
            // services.AddSingleton<IIndexProvider, CourseIndexProvider>();
            // services.AddSingleton<IIndexProvider, LessonIndexProvider>();
            // services.AddSingleton<IIndexProvider, EnrollmentIndexProvider>();

            // Register Admin Menu
            services.AddScoped<INavigationProvider, TrainingAdminMenu>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Public routes for TrainingController
            routes.MapAreaControllerRoute(
                name: "HoangNgoc.Training.Training",
                areaName: "HoangNgoc.Training",
                pattern: "Training/{action=Index}/{id?}",
                defaults: new { controller = "Training" }
            );
        }
    }
}