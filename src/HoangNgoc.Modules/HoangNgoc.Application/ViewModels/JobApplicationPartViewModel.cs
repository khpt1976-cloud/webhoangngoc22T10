using OrchardCore.ContentManagement;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.ViewModels
{
    public class JobApplicationPartViewModel
    {
        public JobApplicationPart JobApplicationPart { get; set; } = new();
        public ContentItem ContentItem { get; set; } = new();
        
        // Form properties for easy binding
        public string ApplicationId { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string ApplicantPhone { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string ExpectedSalary { get; set; } = string.Empty;
        public string ApplicationStatus { get; set; } = "Pending";
        public DateTime? ApplicationDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string InterviewNotes { get; set; } = string.Empty;
        public string InterviewResult { get; set; } = string.Empty;
        public bool IsShortlisted { get; set; }
        public bool IsHired { get; set; }
        public string HRNotes { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string ReferenceContact { get; set; } = string.Empty;
    }
}