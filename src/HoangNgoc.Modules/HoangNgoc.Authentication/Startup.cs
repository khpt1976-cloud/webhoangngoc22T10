using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace HoangNgoc.Authentication;

public class Startup : OrchardCore.Modules.StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // OrchardCore.Users đã cung cấp tất cả authentication services cần thiết
        // Chúng ta chỉ cần đăng ký routes
    }
}