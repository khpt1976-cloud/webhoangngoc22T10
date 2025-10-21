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
                .WithPart("NewsArticlePart", part => part
                    .WithPosition("3")
                )
                .WithPart("CommonPart", part => part
                    .WithPosition("4")
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

            // Create NewsArticlePart definition with fields
            await _contentDefinitionManager.AlterPartDefinitionAsync("NewsArticlePart", part => part
                .WithField("Summary", field => field
                    .OfType("TextField")
                    .WithDisplayName("Tóm tắt")
                    .WithPosition("0")
                )
                .WithField("Author", field => field
                    .OfType("TextField")
                    .WithDisplayName("Tác giả")
                    .WithPosition("1")
                )
                .WithField("IsFeatured", field => field
                    .OfType("BooleanField")
                    .WithDisplayName("Tin nổi bật")
                    .WithPosition("2")
                )
                .WithField("ViewCount", field => field
                    .OfType("NumericField")
                    .WithDisplayName("Lượt xem")
                    .WithPosition("3")
                )
                .WithField("PublishedDate", field => field
                    .OfType("DateTimeField")
                    .WithDisplayName("Ngày xuất bản")
                    .WithPosition("4")
                )
                .WithField("FeaturedImage", field => field
                    .OfType("MediaField")
                    .WithDisplayName("Ảnh đại diện")
                    .WithPosition("5")
                )
            );

            return 1;
        }


    }
}
