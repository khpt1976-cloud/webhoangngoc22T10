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
            services.AddContentPart<JobApplicationPart>();
            services.AddContentPart<JobPostingPart>();
            services.AddContentPart<CandidatePart>();

            // Register Display Drivers
            services.AddScoped<IContentPartDisplayDriver, JobApplicationPartDisplayDriver>();
            services.AddScoped<IContentPartDisplayDriver, JobPostingPartDisplayDriver>();
            services.AddScoped<IContentPartDisplayDriver, CandidatePartDisplayDriver>();

            // Register Services
            services.AddScoped<IApplicationService, ApplicationService>();

            // Register Indexes
            services.AddSingleton<IIndexProvider, JobApplicationIndexProvider>();
            services.AddSingleton<IIndexProvider, JobPostingIndexProvider>();
            services.AddSingleton<IIndexProvider, CandidateIndexProvider>();

            // Register Migrations
            services.AddScoped<IDataMigration, ApplicationMigrations>();

            // Register Navigation
            services.AddScoped<INavigationProvider, AdminMenu>();
        }
    }
}