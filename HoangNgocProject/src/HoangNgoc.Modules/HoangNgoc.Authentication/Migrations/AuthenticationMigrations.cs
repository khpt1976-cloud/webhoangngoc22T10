using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Authentication.Migrations
{
    public class AuthenticationMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public AuthenticationMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create UserProfile content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("UserProfile", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "profiles/{{ Model.ContentItem | display_text | slugify }}",
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
                .Securable()
            );

            return 1;
        }

        public async Task<int> UpdateFrom1()
        {
            // Create UserRole content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("UserRole", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("CommonPart", part => part
                    .WithPosition("1")
                )
                .Creatable()
                .Listable()
                .Draftable()
                .Versionable()
            );

            return 2;
        }
    }
}