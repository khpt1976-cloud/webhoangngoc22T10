using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Security;

namespace HoangNgoc.Comment
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
                .Add(S["Content"], content => content
                    .Add(S["Comments"], S["Comments"].Value, comments => comments
                        .Action("Index", "Admin", new { area = "HoangNgoc.Comment" })
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav()
                    )
                );

            return ValueTask.CompletedTask;
        }
    }
}