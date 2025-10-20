# 📋 CẤU TRÚC CHUẨN CỦA DỰ ÁN ORCHARDCORE

**Tài liệu này được tạo dựa trên phân tích OrchardCore source code v2.2.1 và kinh nghiệm thực tế triển khai thành công.**

## 🏗️ 1. CẤU TRÚC THƯ MỤC TỔNG THỂ

```
YourProject/
├── src/
│   ├── YourProject.Web/                    ← Main Web Application (CLEAN)
│   │   ├── YourProject.Web.csproj
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── wwwroot/
│   ├── YourProject.Modules/                ← Custom Modules (Separate Projects)
│   │   ├── YourModule.Core/
│   │   │   ├── YourModule.Core.csproj
│   │   │   ├── Manifest.cs
│   │   │   ├── Startup.cs
│   │   │   ├── Controllers/
│   │   │   ├── Models/
│   │   │   ├── Services/
│   │   │   ├── Views/
│   │   │   └── wwwroot/
│   │   ├── YourModule.News/
│   │   ├── YourModule.Authentication/
│   │   └── ...
│   ├── YourProject.Themes/                 ← Custom Themes (Separate Projects)
│   │   ├── YourTheme/
│   │   │   ├── YourTheme.csproj
│   │   │   ├── Manifest.cs
│   │   │   ├── Views/
│   │   │   ├── wwwroot/
│   │   │   │   ├── css/
│   │   │   │   ├── js/
│   │   │   │   └── images/
│   │   │   └── Recipes/
│   │   └── ...
│   └── YourProject.Application.Targets/    ← Bundle Package (References All)
│       └── YourProject.Application.Targets.csproj
├── docs/
├── tests/
└── README.md
```

## 🎯 2. MAIN WEB APPLICATION (YourProject.Web)

### ✅ CÁCH ĐÚNG - Main Project .csproj:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>2cfccf50-2ae4-4017-bbd7-a0e453cbf713</UserSecretsId>
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
  </PropertyGroup>

  <!-- Watcher include and excludes -->
  <ItemGroup>
      <Watch Include="**\*.cs" Exclude="Recipes\**;Assets\**;node_modules\**\*;**\*.js.map;obj\**\*;bin\**\*" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <!-- CHỈ reference OrchardCore base -->
  <ItemGroup>
    <PackageReference Include="OrchardCore.Application.Cms.Targets" Version="2.2.1" />
  </ItemGroup>

  <!-- Reference custom bundle -->
  <ItemGroup>
    <ProjectReference Include="../YourProject.Application.Targets/YourProject.Application.Targets.csproj" />
  </ItemGroup>
</Project>
```

### ✅ Program.cs chuẩn:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrchardCms()
    // // Orchard Specific Pipeline
    // .ConfigureServices( services => {
    // })
    // .Configure( (app, routes, services) => {
    // })
;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOrchardCore();

app.Run();
```

### ❌ CÁCH SAI - KHÔNG BAO GIỜ LÀM:

```xml
<!-- KHÔNG BAO GIỜ làm như này -->
<ItemGroup>
  <!-- SAI: Reference trực tiếp modules từ main project -->
  <ProjectReference Include="Modules/Module1/Module1.csproj" />
  <ProjectReference Include="Modules/Module2/Module2.csproj" />
</ItemGroup>
```

```
❌ SAI: Modules nằm trong main project
YourProject.Web/
├── Modules/           ← SAI HOÀN TOÀN
│   ├── Module1/
│   └── Module2/
└── YourProject.Web.csproj
```

## 🧩 3. CUSTOM MODULES (Separate Projects)

### ✅ Module .csproj chuẩn:

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AddRazorSupportForMvc>true</AddRazorSupportForMvc>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <!-- OrchardCore Module Target -->
  <ItemGroup>
    <PackageReference Include="OrchardCore.Module.Targets" Version="2.2.1" />
  </ItemGroup>

  <!-- Dependencies với modules khác -->
  <ItemGroup>
    <ProjectReference Include="../YourModule.Core/YourModule.Core.csproj" />
  </ItemGroup>
</Project>
```

### ✅ Manifest.cs chuẩn:

```csharp
using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Your Module Name",
    Author = "Your Name",
    Website = "https://yourwebsite.com",
    Version = "1.0.0",
    Description = "Description of your module",
    Category = "Content Management",
    Dependencies = new[] { "OrchardCore.Contents", "YourModule.Core" }
)]
```

### ✅ Startup.cs chuẩn:

```csharp
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace YourModule.News
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register your services here
        }
    }
}
```

## 📦 4. BUNDLE TARGETS (YourProject.Application.Targets)

### ✅ Bundle .csproj chuẩn:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <!-- NuGet properties-->
    <Title>YourProject Application Bundle</Title>
    <Description>Bundle containing all custom modules and themes for OrchardCore CMS</Description>
    <PackageTags>OrchardCore YourProject CMS Modules</PackageTags>
  </PropertyGroup>

  <!--
    Reference TẤT CẢ custom modules với PrivateAssets="none"
    Điều này đảm bảo modules được include khi bundle được reference
  -->
  <ItemGroup>
    <!-- Custom Modules - PHẢI có PrivateAssets="none" -->
    <ProjectReference Include="../YourProject.Modules/YourModule.Core/YourModule.Core.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.News/YourModule.News.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.Authentication/YourModule.Authentication.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.Comment/YourModule.Comment.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.Training/YourModule.Training.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.Payment/YourModule.Payment.csproj" PrivateAssets="none" />
    <ProjectReference Include="../YourProject.Modules/YourModule.Application/YourModule.Application.csproj" PrivateAssets="none" />
    
    <!-- Custom Themes - PHẢI có PrivateAssets="none" -->
    <ProjectReference Include="../YourProject.Themes/YourTheme/YourTheme.csproj" PrivateAssets="none" />
  </ItemGroup>
</Project>
```

## 🎨 5. CUSTOM THEMES

### ✅ Theme .csproj chuẩn:

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <AddRazorSupportForMvc>true</AddRazorSupportForMvc>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <!-- OrchardCore Theme Target -->
  <ItemGroup>
    <PackageReference Include="OrchardCore.Theme.Targets" Version="2.2.1" />
  </ItemGroup>
</Project>
```

### ✅ Theme Manifest.cs chuẩn:

```csharp
using OrchardCore.DisplayManagement.Manifest;

[assembly: Theme(
    Name = "Your Theme Name",
    Author = "Your Name",
    Website = "https://yourwebsite.com",
    Version = "2.0.0",
    Description = "Description of your theme",
    Tags = new[] { "Bootstrap", "Responsive" }
)]
```

## ⚡ 6. NGUYÊN TẮC QUAN TRỌNG

### ✅ ĐÚNG:
1. **Main project CLEAN** - chỉ reference OrchardCore base và custom bundle
2. **Modules là projects riêng biệt** - không nằm trong main project
3. **Bundle reference tất cả** với `PrivateAssets="none"`
4. **Mỗi module có Manifest.cs** riêng để định nghĩa metadata
5. **Dependencies qua ProjectReference** giữa modules
6. **Themes là projects riêng biệt** như modules
7. **Sử dụng OrchardCore.Module.Targets** cho modules
8. **Sử dụng OrchardCore.Theme.Targets** cho themes

### ❌ SAI:
1. ❌ Copy source code modules vào main project
2. ❌ Reference trực tiếp modules từ main project  
3. ❌ Modules nằm trong thư mục con của main project
4. ❌ Thiếu bundle targets
5. ❌ Duplicate module loading (vừa có source vừa có reference)
6. ❌ Thiếu `PrivateAssets="none"` trong bundle
7. ❌ Sử dụng sai SDK targets

## 🔄 7. WORKFLOW PHÁT TRIỂN

### Khi thêm module mới:
1. Tạo project riêng trong `YourProject.Modules/NewModule/`
2. Tạo `NewModule.csproj` với OrchardCore.Module.Targets
3. Tạo `Manifest.cs` để định nghĩa module
4. Tạo `Startup.cs` để register services
5. Thêm ProjectReference vào Bundle với `PrivateAssets="none"`
6. Build và test
7. Module tự động xuất hiện trong Features admin

### Khi thêm theme mới:
1. Tạo project riêng trong `YourProject.Themes/NewTheme/`
2. Tạo `NewTheme.csproj` với OrchardCore.Theme.Targets
3. Tạo `Manifest.cs` để định nghĩa theme
4. Thêm ProjectReference vào Bundle với `PrivateAssets="none"`
5. Build và test
6. Theme tự động xuất hiện trong admin

### Khi debug vấn đề:
1. Kiểm tra cấu trúc thư mục đúng pattern
2. Kiểm tra Bundle có reference đầy đủ modules/themes
3. Kiểm tra `PrivateAssets="none"` trong Bundle
4. Kiểm tra Manifest.cs của từng module/theme
5. Build clean và restart application

## 🎯 8. VÍ DỤ THỰC TẾ THÀNH CÔNG

**Dự án HoangNgocProject đã triển khai thành công theo cấu trúc này:**

```
HoangNgocProject/
├── src/
│   ├── HoangNgocCMS.Web/                   ← Main project (CLEAN)
│   ├── HoangNgoc.Modules/                  ← 8 modules riêng biệt
│   │   ├── HoangNgoc.Core/
│   │   ├── HoangNgoc.News/
│   │   ├── HoangNgoc.Authentication/
│   │   ├── HoangNgoc.Comment/
│   │   ├── HoangNgoc.Training/
│   │   ├── HoangNgoc.Payment/
│   │   ├── HoangNgoc.Application/
│   │   └── HoangNgoc.Simple/
│   ├── HoangNgoc.Themes/                   ← Theme riêng biệt
│   │   └── HoangNgoc/
│   └── HoangNgoc.Application.Targets/      ← Bundle tất cả
```

**Kết quả:**
- ✅ Build thành công: 124 warnings, 0 errors
- ✅ Tất cả 8 modules hiển thị trong Features
- ✅ Không có lỗi duplicate module attribute
- ✅ OrchardCore nhận diện và load đúng tất cả modules
- ✅ Website chạy ổn định trên port 45401

## 🚨 9. LỖI THƯỜNG GẶP VÀ CÁCH KHẮC PHỤC

### Lỗi: "Duplicate 'Module' attribute"
**Nguyên nhân:** Vừa có source code modules trong main project VÀ vừa có ProjectReference
**Khắc phục:** Di chuyển modules ra ngoài main project, chỉ giữ ProjectReference qua Bundle

### Lỗi: Modules không hiển thị trong Features
**Nguyên nhân:** Thiếu `PrivateAssets="none"` trong Bundle hoặc sai cấu trúc
**Khắc phục:** Kiểm tra Bundle reference và cấu trúc thư mục

### Lỗi: Build failed với module references
**Nguyên nhân:** Sai SDK targets hoặc thiếu dependencies
**Khắc phục:** Sử dụng đúng OrchardCore.Module.Targets và OrchardCore.Theme.Targets

## 📚 10. TÀI LIỆU THAM KHẢO

- **OrchardCore Official Source:** https://github.com/OrchardCMS/OrchardCore
- **OrchardCore Documentation:** https://docs.orchardcore.net/
- **Module Development Guide:** https://docs.orchardcore.net/en/latest/docs/guides/create-modular-application-mvc/
- **Theme Development Guide:** https://docs.orchardcore.net/en/latest/docs/reference/core/Placement/

---

**📝 Ghi chú:** Tài liệu này được tạo dựa trên kinh nghiệm thực tế triển khai thành công OrchardCore v2.2.1 với 8 custom modules và 1 custom theme. Cấu trúc này đã được verify hoạt động hoàn hảo và tuân thủ đúng pattern của OrchardCore.

**🗓️ Ngày tạo:** 20/10/2025  
**📧 Liên hệ:** Khpt1976@gmail.com  
**🔄 Phiên bản:** 1.0