using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "HoangNgoc News",
    Author = "HoangNgoc Team",
    Website = "https://hoangngocdevelopment.com",
    Version = "1.0.0",
    Description = "Advanced News management module leveraging full Orchard Core capabilities",
    Category = "Content Management",
    Dependencies = new[] { 
        "HoangNgoc.Core", 
        "OrchardCore.ContentManagement",
        "OrchardCore.ContentFields",
        "OrchardCore.Autoroute",
        "OrchardCore.Taxonomies",
        "OrchardCore.Media",
        "OrchardCore.Indexing",
        "OrchardCore.Liquid",
        "OrchardCore.Workflows"
    }
)]
