using OrchardCore.Media.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.JobPosting.Models
{
    public class JobPosting : ContentPart
    {
        public TextField Title { get; set; } = new();
        public HtmlField Description { get; set; } = new();
        public TextField Company { get; set; } = new();
        public TextField Location { get; set; } = new();
        public TextField JobType { get; set; } = new(); // Full-time, Part-time, Contract, etc.
        public TextField ExperienceLevel { get; set; } = new(); // Entry, Mid, Senior
        public NumericField MinSalary { get; set; } = new();
        public NumericField MaxSalary { get; set; } = new();
        public TextField Currency { get; set; } = new();
        public TextField SalaryType { get; set; } = new(); // Monthly, Yearly, Hourly
        public HtmlField Requirements { get; set; } = new();
        public HtmlField Benefits { get; set; } = new();
        public TextField Skills { get; set; } = new();
        public TextField Category { get; set; } = new();
        public TextField Industry { get; set; } = new();
        public BooleanField IsRemote { get; set; } = new();
        public BooleanField IsUrgent { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public DateTimeField ApplicationDeadline { get; set; } = new();
        public DateTimeField StartDate { get; set; } = new();
        public TextField ContactEmail { get; set; } = new();
        public TextField ContactPhone { get; set; } = new();
        public TextField ContactPerson { get; set; } = new();
        public TextField CompanyWebsite { get; set; } = new();
        public MediaField CompanyLogo { get; set; } = new();
        public NumericField ViewCount { get; set; } = new();
        public NumericField ApplicationCount { get; set; } = new();
        public BooleanField IsActive { get; set; } = new();
        public TextField PostedBy { get; set; } = new();
        public DateTimeField PostedDate { get; set; } = new();
        public DateTimeField LastModified { get; set; } = new();
    }

    public class JobApplication : ContentPart
    {
        public TextField JobPostingId { get; set; } = new();
        public TextField ApplicantId { get; set; } = new();
        public TextField ApplicantName { get; set; } = new();
        public TextField ApplicantEmail { get; set; } = new();
        public TextField ApplicantPhone { get; set; } = new();
        public HtmlField CoverLetter { get; set; } = new();
        public MediaField Resume { get; set; } = new();
        public TextField Status { get; set; } = new(); // Applied, Reviewed, Interview, Rejected, Hired
        public DateTimeField ApplicationDate { get; set; } = new();
        public DateTimeField ReviewedDate { get; set; } = new();
        public TextField ReviewedBy { get; set; } = new();
        public HtmlField ReviewNotes { get; set; } = new();
        public DateTimeField InterviewDate { get; set; } = new();
        public TextField InterviewType { get; set; } = new(); // Phone, Video, In-person
        public TextField InterviewLocation { get; set; } = new();
        public HtmlField InterviewNotes { get; set; } = new();
        public NumericField Rating { get; set; } = new();
        public BooleanField IsShortlisted { get; set; } = new();
    }
}