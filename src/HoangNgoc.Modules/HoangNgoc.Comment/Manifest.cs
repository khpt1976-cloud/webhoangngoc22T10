using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "HoangNgoc Comment",
    Author = "HoangNgoc",
    Website = "https://hoangngocdevelopment.com",
    Version = "1.0.0",
    Description = "Comment management module for HoangNgoc website with Orchard Core integration",
    Category = "Content Management",
    Dependencies = new[] { "HoangNgoc.Core", "OrchardCore.ContentManagement", "OrchardCore.ContentFields", "OrchardCore.Users" }
)]
