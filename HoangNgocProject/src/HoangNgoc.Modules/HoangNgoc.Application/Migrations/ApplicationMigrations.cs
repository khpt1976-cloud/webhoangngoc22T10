using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Application.Migrations
{
    public class ApplicationMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ApplicationMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create JobPosting content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("JobPosting", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "jobs/{{ Model.ContentItem | display_text | slugify }}",
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

            // Create JobApplication content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("JobApplication", type => type
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
                .Securable()
            );

            // Create Candidate content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Candidate", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "candidates/{{ Model.ContentItem | display_text | slugify }}",
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

            // Create JobCategory content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("JobCategory", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "job-categories/{{ Model.ContentItem | display_text | slugify }}",
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

            // Shortcut other migration steps on new installations - OrchardCore pattern
            return 4;
        }

        public async Task<int> UpdateFrom1Async()
        {
            // Create JobApplication content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("JobApplication", type => type
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
                .Securable()
            );

            return 2;
        }

        public async Task<int> UpdateFrom2Async()
        {
            // Create Candidate content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Candidate", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "candidates/{{ Model.ContentItem | display_text | slugify }}",
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

            return 3;
        }

        public async Task<int> UpdateFrom3Async()
        {
            // Create JobCategory content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("JobCategory", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "job-categories/{{ Model.ContentItem | display_text | slugify }}",
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

            return 4;
        }
    }
}