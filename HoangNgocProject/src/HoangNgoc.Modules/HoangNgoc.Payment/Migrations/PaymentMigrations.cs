using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Autoroute.Models;

namespace HoangNgoc.Payment.Migrations
{
    public class PaymentMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public PaymentMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            // Create Payment content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Payment", type => type
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

            return 1;
        }

        public async Task<int> UpdateFrom1()
        {
            // Create Order content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Order", type => type
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

        public async Task<int> UpdateFrom2()
        {
            // Create Invoice content type
            await _contentDefinitionManager.AlterTypeDefinitionAsync("Invoice", type => type
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("1")
                    .WithSettings(new AutoroutePartSettings
                    {
                        Pattern = "invoices/{{ Model.ContentItem | display_text | slugify }}",
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
    }
}
