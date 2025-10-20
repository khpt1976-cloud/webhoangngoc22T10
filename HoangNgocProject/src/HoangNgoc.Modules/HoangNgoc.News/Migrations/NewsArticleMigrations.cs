using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.News.Migrations
{
    public class NewsArticleMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public NewsArticleMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create NewsArticle content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("NewsArticle", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "news/{{ Model.ContentItem | display_text | slugify }}",
                        AllowCustomPath = true
                    })
                )
                .WithPart("HtmlBodyPart", part => part
                    .WithPosition("2")
                )
                .WithPart("CommonPart", part => part
                    .WithPosition("3")
                )
                .Creatable()
                .Listable()
                .Draftable()
                .Versionable()
                .Securable()
            );

            // Create NewsCategory content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("NewsCategory", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "news-categories/{{ Model.ContentItem | display_text | slugify }}",
                        AllowCustomPath = true
                    })
                )
                .WithPart("CommonPart", part => part
                    .WithPosition("2")
                )
                .Creatable()
                .Listable()
                .Draftable()
                .Versionable()
            );

            return 1;
        }


    }
}
