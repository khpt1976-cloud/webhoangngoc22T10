using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace HoangNgoc.Simple;

public class Startup : OrchardCore.Modules.StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Simple module - no services needed
    }
}