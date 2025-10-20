namespace HoangNgocCMS.Web.Models
{
    public class JobApplication
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string JobId { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string ApplicantPhone { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string ExpectedSalary { get; set; } = string.Empty;
        public string ApplicationStatus { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? InterviewDate { get; set; }
        public string? InterviewNotes { get; set; }
        public string? InterviewResult { get; set; }
        public bool IsShortlisted { get; set; }
        public bool IsHired { get; set; }
        public string? HRNotes { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? ReferenceContact { get; set; }
    }
}