using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Workflows.Helpers;
using HoangNgoc.News.Drivers;
using HoangNgoc.News.Models;
using HoangNgoc.News.Migrations;
using HoangNgoc.News.Services;
using HoangNgoc.News.AdminMenu;
using HoangNgoc.News.Indexes;
using HoangNgoc.News.Activities;
using HoangNgoc.News.Handlers;
using YesSql.Indexes;

namespace HoangNgoc.News;

public class Startup : OrchardCore.Modules.StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Register ContentPart
        services.AddContentPart<NewsArticlePart>();
        
        // Register Display Driver
        services.AddScoped<IContentPartDisplayDriver, NewsArticlePartDisplayDriver>();
        
        // Register Migrations
        services.AddScoped<IDataMigration, NewsArticleMigrations>();
        
        // Register Services
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<INewsSearchService, NewsSearchService>();
        
        // Register Index Provider
        services.AddSingleton<IIndexProvider, NewsArticleIndexProvider>();
        
        // Register Admin Menu
        services.AddScoped<INavigationProvider, NewsAdminMenu>();
        
        // Register Workflow Activities
        services.AddScoped<NewsPublishedActivity>();
        services.AddScoped<NewsUnpublishedActivity>();
        services.AddScoped<IncrementNewsViewCountActivity>();
        services.AddScoped<SendNewsNotificationActivity>();
        
        // Register Content Handlers
        services.AddScoped<IContentHandler, NewsWorkflowHandler>();
    }
}