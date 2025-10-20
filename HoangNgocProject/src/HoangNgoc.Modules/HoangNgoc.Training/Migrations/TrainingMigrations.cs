using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Training.Migrations
{
    public class TrainingMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TrainingMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create Course content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Course", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "courses/{{ Model.ContentItem | display_text | slugify }}",
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

            // Create Lesson content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Lesson", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "lessons/{{ Model.ContentItem | display_text | slugify }}",
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

            // Create Enrollment content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Enrollment", type => type
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

            // Create CourseCategory content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("CourseCategory", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "course-categories/{{ Model.ContentItem | display_text | slugify }}",
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
            );

            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            // Force recreate all content types to ensure they exist
            
            // Create Lesson content type (if not exists)
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Lesson", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "lessons/{{ Model.ContentItem | display_text | slugify }}",
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

            // Create Enrollment content type (if not exists)
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Enrollment", type => type
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

            // Create CourseCategory content type (if not exists)
            await _contentDefinitionManager.AlterTypeDefinitionAsync("CourseCategory", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "course-categories/{{ Model.ContentItem | display_text | slugify }}",
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
            );

            return 2;
        }

    }
}
