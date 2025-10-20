using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCore.Security;

namespace HoangNgoc.Payment.AdminMenu
{
    public class PaymentAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public PaymentAdminMenu(IStringLocalizer<PaymentAdminMenu> localizer)
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
                .Add(S["Payment"], "10", payment => payment
                    .AddClass("payment").Id("payment")
                    .Add(S["Payments"], "1", payments => payments
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Payment" })
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Orders"], "2", orders => orders
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Order" })
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Invoices"], "3", invoices => invoices
                        .Action("List", "Admin", new { area = "OrchardCore.Contents", contentTypeId = "Invoice" })
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                    .Add(S["Payment Reports"], "4", reports => reports
                        .Action("Index", "PaymentReports", new { area = "HoangNgoc.Payment" })
                        .Permission(StandardPermissions.SiteOwner)
                        .LocalNav())
                );

            return ValueTask.CompletedTask;
        }
    }
}