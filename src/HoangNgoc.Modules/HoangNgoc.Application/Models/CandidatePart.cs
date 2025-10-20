using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace HoangNgoc.Application.Models
{
    public class CandidatePart : ContentPart
    {
        public TextField CandidateId { get; set; } = new();
        public TextField FullName { get; set; } = new();
        public TextField Email { get; set; } = new();
        public TextField Phone { get; set; } = new();
        public DateField DateOfBirth { get; set; } = new();
        public TextField Address { get; set; } = new();
        public TextField City { get; set; } = new();
        public TextField Country { get; set; } = new();
        public TextField Nationality { get; set; } = new();
        public TextField Gender { get; set; } = new();
        public TextField MaritalStatus { get; set; } = new();
        public MediaField ProfilePhoto { get; set; } = new();
        public MediaField Resume { get; set; } = new();
        public MediaField CoverLetter { get; set; } = new();
        public TextField CurrentPosition { get; set; } = new();
        public TextField CurrentCompany { get; set; } = new();
        public TextField TotalExperience { get; set; } = new();
        public TextField Skills { get; set; } = new();
        public TextField Education { get; set; } = new();
        public TextField Certifications { get; set; } = new();
        public TextField Languages { get; set; } = new();
        public TextField ExpectedSalary { get; set; } = new();
        public TextField NoticePeriod { get; set; } = new();
        public TextField PreferredLocation { get; set; } = new();
        public BooleanField IsAvailable { get; set; } = new();
        public TextField CandidateSource { get; set; } = new();
        public HtmlField Notes { get; set; } = new();
        public DateTimeField RegistrationDate { get; set; } = new();
        public DateTimeField LastUpdated { get; set; } = new();
    }
}