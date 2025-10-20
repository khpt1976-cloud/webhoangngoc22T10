using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System.Threading.Tasks;

namespace HoangNgoc.News.AdminMenu;

public class NewsAdminMenu : INavigationProvider
{
    private readonly IStringLocalizer S;

    public NewsAdminMenu(IStringLocalizer<NewsAdminMenu> localizer)
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
            .Add(S["Content"], NavigationConstants.AdminMenuContentPosition, content => content
                .Add(S["News"], "10", news => news
                    .Add(S["All News Articles"], "1", articles => articles
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "NewsArticle" })
                        .Permission(OrchardCore.Contents.CommonPermissions.AccessContentApi)
                        .LocalNav()
                    )
                    .Add(S["Create News Article"], "2", create => create
                        .Action("Create", "Admin", new { area = "OrchardCore.Contents", id = "NewsArticle" })
                        .Permission(OrchardCore.Contents.CommonPermissions.EditContent)
                        .LocalNav()
                    )
                    .Add(S["News Categories"], "3", categories => categories
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "NewsCategory" })
                        .Permission(OrchardCore.Contents.CommonPermissions.AccessContentApi)
                        .LocalNav()
                    )
                    .Add(S["Create Category"], "4", createCat => createCat
                        .Action("Create", "Admin", new { area = "OrchardCore.Contents", id = "NewsCategory" })
                        .Permission(OrchardCore.Contents.CommonPermissions.EditContent)
                        .LocalNav()
                    )
                )
            );

        return ValueTask.CompletedTask;
    }
}