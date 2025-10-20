using OrchardCore.Media.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.UserProfile.Models
{
    public class UserProfile : ContentPart
    {
        public TextField FirstName { get; set; } = new();
        public TextField LastName { get; set; } = new();
        public TextField Email { get; set; } = new();
        public TextField Phone { get; set; } = new();
        public TextField Address { get; set; } = new();
        public TextField City { get; set; } = new();
        public TextField Country { get; set; } = new();
        public TextField JobTitle { get; set; } = new();
        public TextField Company { get; set; } = new();
        public HtmlField Bio { get; set; } = new();
        public TextField Skills { get; set; } = new();
        public TextField Experience { get; set; } = new();
        public TextField Education { get; set; } = new();
        public TextField LinkedInUrl { get; set; } = new();
        public TextField GitHubUrl { get; set; } = new();
        public TextField WebsiteUrl { get; set; } = new();
        public MediaField Avatar { get; set; } = new();
        public MediaField Resume { get; set; } = new();
        public BooleanField IsPublic { get; set; } = new();
        public BooleanField IsAvailableForHire { get; set; } = new();
        public NumericField SalaryExpectation { get; set; } = new();
        public TextField PreferredLocation { get; set; } = new();
        public TextField Languages { get; set; } = new();
        public DateTimeField LastLoginDate { get; set; } = new();
        public DateTimeField ProfileCompletedDate { get; set; } = new();
    }
}