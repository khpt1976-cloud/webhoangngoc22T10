using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Security;
using OrchardCore.Mvc.Core.Utilities;
using HoangNgoc.Application.Controllers;

namespace HoangNgoc.Application
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public ValueTask BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return ValueTask.CompletedTask;
            }

            builder
                .Add(S["Application Management"], "10", application => application
                    .AddClass("application").Id("application")
                    .Add(S["Job Applications"], "1", applications => applications
                        .Action("Index", "Application", "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Job Postings"], "2", jobs => jobs
                        .Action(nameof(AdminController.Index), typeof(AdminController).ControllerName(), "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Candidates"], "3", candidates => candidates
                        .Action("Index", "Candidate", "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Active Jobs"], "4", activeJobs => activeJobs
                        .Action(nameof(AdminController.Active), typeof(AdminController).ControllerName(), "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Draft Jobs"], "5", draftJobs => draftJobs
                        .Action(nameof(AdminController.Draft), typeof(AdminController).ControllerName(), "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Available Candidates"], "6", availableCandidates => availableCandidates
                        .Action("Available", "Candidate", "HoangNgoc.Application")
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                );

            return ValueTask.CompletedTask;
        }
    }
}