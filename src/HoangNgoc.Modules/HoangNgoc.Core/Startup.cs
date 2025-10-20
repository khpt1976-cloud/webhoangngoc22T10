using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Indexing;
using OrchardCore.Workflows.Activities;

// Core Services
using HoangNgoc.Core.Abstractions;
using HoangNgoc.Core.Services;
using HoangNgoc.Core.Permissions;

namespace HoangNgoc.Core;

public class Startup : OrchardCore.Modules.StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Core Services
        services.AddScoped<IPermissionProvider, CorePermissions>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
        services.AddScoped<IWalletTransactionProcessor, WalletTransactionProcessor>();
    }
}