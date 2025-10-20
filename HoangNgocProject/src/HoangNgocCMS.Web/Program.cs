using HoangNgocCMS.Web.Services;
using OrchardCore.Email;
using HoangNgoc.UserProfile.Services;
using HoangNgoc.JobPosting.Services;
using HoangNgoc.Course.Services;
using HoangNgoc.Event.Services;
using HoangNgoc.NewsArticle.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrchardCms()
    .ConfigureServices(services => {
        // Register custom services
        services.AddScoped<ICustomEmailService, CustomEmailService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
        services.AddScoped<HoangNgocCMS.Web.Services.IUserJobService, HoangNgocCMS.Web.Services.UserJobService>();
        services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
        services.AddScoped<IEventRegistrationService, EventRegistrationService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<HoangNgoc.NewsArticle.Services.IArticleRatingService, HoangNgoc.NewsArticle.Services.ArticleRatingService>();
        
        // Configure email settings
        services.Configure<HoangNgocCMS.Web.Services.EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        
        // Add authentication services
        services.AddAuthentication()
            .AddGoogle(options => {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            })
            .AddFacebook(options => {
                options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            });
            // .AddGitHub(options => {
            //     options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
            //     options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
            // });
            
        // Configure Identity options
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            
            // User settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false; // Set to true in production
            
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
        
        // Add CORS for API endpoints
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        
        // Register custom services
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
        services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
        services.AddScoped<IEventRegistrationService, EventRegistrationService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<HoangNgocCMS.Web.Services.IUserJobService, HoangNgocCMS.Web.Services.UserJobService>();
        services.AddScoped<HoangNgocCMS.Web.Services.IArticleRatingService, HoangNgocCMS.Web.Services.ArticleRatingService>();
        services.AddScoped<HoangNgocCMS.Web.Services.CustomEmailService>();
    })
    .Configure((app, routes, services) => {
        // Configure custom routes
        routes.MapAreaControllerRoute(
            name: "Jobs",
            areaName: "",
            pattern: "jobs/{action=Index}/{id?}",
            defaults: new { controller = "Job" }
        );
        
        routes.MapAreaControllerRoute(
            name: "Account",
            areaName: "",
            pattern: "account/{action=Login}/{id?}",
            defaults: new { controller = "Account" }
        );
        
        routes.MapAreaControllerRoute(
            name: "Courses",
            areaName: "",
            pattern: "courses/{action=Index}/{id?}",
            defaults: new { controller = "Course" }
        );
        
        routes.MapAreaControllerRoute(
            name: "Events",
            areaName: "",
            pattern: "events/{action=Index}/{id?}",
            defaults: new { controller = "Event" }
        );
        
        routes.MapAreaControllerRoute(
            name: "News",
            areaName: "",
            pattern: "news/{action=Index}/{id?}",
            defaults: new { controller = "News" }
        );
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable CORS
app.UseCors("AllowAll");

// Add security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    await next();
});

app.UseOrchardCore();

app.Run();