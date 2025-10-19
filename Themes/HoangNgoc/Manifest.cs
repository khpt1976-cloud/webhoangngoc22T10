using OrchardCore.DisplayManagement.Manifest;

[assembly: Theme(
    Name = "HoangNgoc Professional Theme",
    Author = "HoangNgoc Development Team",
    Website = "https://hoangngoc.com",
    Version = "2.0.0",
    Description = "Advanced responsive theme for HoangNgoc OrchardCore CMS with Bootstrap 5.3, supporting 8 integrated modules: Users, Payment, Training, Application, Comment, News, Authentication, and Core services",
    Tags = new[] { "responsive", "bootstrap5", "modern", "business", "ecommerce", "training", "payment", "professional", "mobile-first", "accessible" },
    BaseTheme = "TheTheme",
    Dependencies = new[] { 
        "OrchardCore.Users",
        "OrchardCore.Roles", 
        "OrchardCore.Contents",
        "OrchardCore.ContentFields",
        "OrchardCore.Html",
        "OrchardCore.Media",
        "OrchardCore.Navigation",
        "OrchardCore.Liquid",
        "OrchardCore.Resources",
        "HoangNgoc.Core",
        "HoangNgoc.Users", 
        "HoangNgoc.Payment",
        "HoangNgoc.Training",
        "HoangNgoc.Application",
        "HoangNgoc.Comment",
        "HoangNgoc.News",
        "HoangNgoc.Authentication",
        "HoangNgoc.Simple"
    }
)]