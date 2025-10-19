using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace HoangNgoc.Application.Models
{
    public class JobApplicationPart : ContentPart
    {
        public TextField ApplicationId { get; set; } = new();
        public TextField JobTitle { get; set; } = new();
        public TextField ApplicantName { get; set; } = new();
        public TextField ApplicantEmail { get; set; } = new();
        public TextField ApplicantPhone { get; set; } = new();
        public HtmlField CoverLetter { get; set; } = new();
        public MediaField Resume { get; set; } = new();
        public TextField Experience { get; set; } = new();
        public TextField Skills { get; set; } = new();
        public TextField Education { get; set; } = new();
        public TextField ExpectedSalary { get; set; } = new();
        public TextField ApplicationStatus { get; set; } = new();
        public DateTimeField ApplicationDate { get; set; } = new();
        public DateTimeField InterviewDate { get; set; } = new();
        public HtmlField InterviewNotes { get; set; } = new();
        public TextField InterviewResult { get; set; } = new();
        public BooleanField IsShortlisted { get; set; } = new();
        public BooleanField IsHired { get; set; } = new();
        public TextField HRNotes { get; set; } = new();
        public TextField Department { get; set; } = new();
        public TextField Position { get; set; } = new();
        public TextField ReferenceContact { get; set; } = new();
    }
}