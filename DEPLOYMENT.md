# 🚀 Hướng dẫn triển khai HoangNgoc Theme

## 📋 Yêu cầu hệ thống

### Môi trường phát triển
- **.NET 8.0 SDK** hoặc cao hơn
- **OrchardCore 2.2.1** framework
- **SQL Server** hoặc **SQLite** database
- **Visual Studio 2022** hoặc **VS Code**
- **Node.js 18+** (cho build assets)

### Môi trường production
- **Windows Server 2019+** hoặc **Linux**
- **IIS 10+** hoặc **Nginx**
- **SQL Server 2019+** hoặc **PostgreSQL**
- **SSL Certificate** (khuyến nghị)

## 🔧 Cài đặt và cấu hình

### 1. Clone repository
```bash
git clone https://github.com/khpt1976-cloud/HoangNgocWeb.git
cd HoangNgocWeb
```

### 2. Cài đặt dependencies
```bash
# Restore .NET packages
dotnet restore

# Install Node.js packages (nếu có)
npm install
```

### 3. Cấu hình database
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HoangNgocDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "OrchardCore": {
    "OrchardCore_Admin": {
      "AdminUrlPrefix": "admin"
    },
    "OrchardCore_Themes": {
      "Site": "HoangNgoc"
    }
  }
}
```

### 4. Khởi tạo database
```bash
dotnet run
# Truy cập http://localhost:5000 để setup
```

## 🎨 Kích hoạt HoangNgoc Theme

### Bước 1: Truy cập Admin Panel
1. Đăng nhập admin: `http://localhost:5000/admin`
2. Username/Password: theo setup ban đầu

### Bước 2: Kích hoạt Theme
1. Vào **Configuration** → **Themes**
2. Tìm **HoangNgoc Theme**
3. Click **Enable**
4. Click **Set as Default**

### Bước 3: Cấu hình Modules
Kích hoạt các modules cần thiết:
- ✅ OrchardCore.Contents
- ✅ OrchardCore.Liquid
- ✅ OrchardCore.Media
- ✅ OrchardCore.Navigation
- ✅ OrchardCore.Users
- ✅ OrchardCore.Roles
- ✅ OrchardCore.Settings

## 📁 Cấu trúc dự án sau khi triển khai

```
HoangNgocWeb/
├── 📁 Themes/
│   └── 📁 HoangNgoc/           # Theme chính
│       ├── Manifest.cs
│       ├── Placement.json
│       ├── Views/
│       └── wwwroot/
├── 📁 Modules/                 # Custom modules (nếu có)
├── 📁 wwwroot/                 # Static files
├── 📁 App_Data/               # OrchardCore data
├── appsettings.json
└── Program.cs
```

## 🔐 Cấu hình bảo mật

### 1. HTTPS Configuration
```json
// appsettings.json
{
  "Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://localhost:5001"
      }
    }
  }
}
```

### 2. Security Headers
```csharp
// Program.cs
app.UseSecurityHeaders(policies =>
{
    policies.AddFrameOptionsDeny()
            .AddXssProtectionBlock()
            .AddContentTypeOptionsNoSniff()
            .AddReferrerPolicyStrictOriginWhenCrossOrigin();
});
```

### 3. Content Security Policy
```html
<!-- Layout.liquid -->
<meta http-equiv="Content-Security-Policy" 
      content="default-src 'self'; 
               script-src 'self' 'unsafe-inline' cdnjs.cloudflare.com;
               style-src 'self' 'unsafe-inline' fonts.googleapis.com;
               font-src 'self' fonts.gstatic.com;">
```

## 🚀 Triển khai Production

### Option 1: IIS Deployment

#### 1. Publish application
```bash
dotnet publish -c Release -o ./publish
```

#### 2. Cấu hình IIS
```xml
<!-- web.config -->
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <handlers>
      <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
    </handlers>
    <aspNetCore processPath="dotnet" 
                arguments=".\HoangNgocWeb.dll" 
                stdoutLogEnabled="false" 
                stdoutLogFile=".\logs\stdout" />
  </system.webServer>
</configuration>
```

#### 3. Application Pool Settings
- **.NET CLR Version**: No Managed Code
- **Managed Pipeline Mode**: Integrated
- **Identity**: ApplicationPoolIdentity

### Option 2: Docker Deployment

#### 1. Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HoangNgocWeb.csproj", "."]
RUN dotnet restore "./HoangNgocWeb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HoangNgocWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HoangNgocWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HoangNgocWeb.dll"]
```

#### 2. Docker Compose
```yaml
version: '3.8'
services:
  web:
    build: .
    ports:
      - "80:80"
      - "443:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=HoangNgocDb;User=sa;Password=YourPassword123!
    depends_on:
      - db
    
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

### Option 3: Azure App Service

#### 1. Azure CLI Deployment
```bash
# Login to Azure
az login

# Create resource group
az group create --name HoangNgocRG --location "Southeast Asia"

# Create App Service plan
az appservice plan create --name HoangNgocPlan --resource-group HoangNgocRG --sku B1

# Create web app
az webapp create --resource-group HoangNgocRG --plan HoangNgocPlan --name hoangngoc-web

# Deploy code
az webapp deployment source config-zip --resource-group HoangNgocRG --name hoangngoc-web --src ./publish.zip
```

#### 2. Application Settings
```json
{
  "ASPNETCORE_ENVIRONMENT": "Production",
  "ConnectionStrings__DefaultConnection": "Server=tcp:hoangngoc-server.database.windows.net,1433;Database=HoangNgocDb;User ID=admin;Password=YourPassword123!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
  "OrchardCore__OrchardCore_Media_Azure__ConnectionString": "DefaultEndpointsProtocol=https;AccountName=hoangngoc;AccountKey=...",
  "OrchardCore__OrchardCore_Media_Azure__ContainerName": "media"
}
```

## 📊 Monitoring và Logging

### 1. Application Insights
```json
// appsettings.json
{
  "ApplicationInsights": {
    "InstrumentationKey": "your-instrumentation-key"
  }
}
```

### 2. Serilog Configuration
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/hoangngoc-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ]
  }
}
```

### 3. Health Checks
```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddUrlGroup(new Uri("https://api.hoangngoc.com/health"), "API Health");

app.MapHealthChecks("/health");
```

## 🔧 Performance Optimization

### 1. Response Caching
```csharp
// Program.cs
builder.Services.AddResponseCaching();
app.UseResponseCaching();
```

### 2. Static File Caching
```csharp
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000");
    }
});
```

### 3. Compression
```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
```

## 🔍 Troubleshooting

### Common Issues

#### 1. Theme không hiển thị
```bash
# Kiểm tra theme status
dotnet run -- themes list

# Reset theme cache
dotnet run -- reset
```

#### 2. Database connection errors
```bash
# Test connection
dotnet ef database update

# Check connection string
dotnet run -- validate-connection
```

#### 3. Permission issues
```bash
# Set folder permissions (Linux)
chmod -R 755 /var/www/hoangngoc
chown -R www-data:www-data /var/www/hoangngoc

# IIS permissions (Windows)
icacls "C:\inetpub\wwwroot\hoangngoc" /grant "IIS_IUSRS:(OI)(CI)F"
```

### Debug Commands
```bash
# Enable detailed logging
export ASPNETCORE_ENVIRONMENT=Development

# Check OrchardCore status
dotnet run -- status

# Validate configuration
dotnet run -- validate

# Clear all caches
dotnet run -- reset --include-media
```

## 📞 Support và Maintenance

### Regular Maintenance Tasks
1. **Weekly**: Check logs for errors
2. **Monthly**: Update dependencies
3. **Quarterly**: Security audit
4. **Yearly**: Performance review

### Backup Strategy
```bash
# Database backup
sqlcmd -S server -E -Q "BACKUP DATABASE HoangNgocDb TO DISK='C:\Backups\HoangNgoc.bak'"

# Media files backup
robocopy "C:\inetpub\wwwroot\hoangngoc\App_Data\Sites\Default\media" "C:\Backups\media" /MIR

# Configuration backup
copy "C:\inetpub\wwwroot\hoangngoc\appsettings.json" "C:\Backups\config\"
```

### Update Process
```bash
# 1. Backup current version
# 2. Pull latest changes
git pull origin main

# 3. Update dependencies
dotnet restore

# 4. Run migrations
dotnet ef database update

# 5. Restart application
systemctl restart hoangngoc-web
```

---

**🎯 Deployment completed successfully!**

Truy cập website tại: `https://your-domain.com`
Admin panel: `https://your-domain.com/admin`