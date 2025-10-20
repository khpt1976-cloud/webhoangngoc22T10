using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace HoangNgoc.Application.Models
{
    public class JobPostingPart : ContentPart
    {
        public TextField JobId { get; set; } = new();
        public TextField JobTitle { get; set; } = new();
        public HtmlField JobDescription { get; set; } = new();
        public HtmlField Requirements { get; set; } = new();
        public HtmlField Benefits { get; set; } = new();
        public TextField Department { get; set; } = new();
        public TextField Location { get; set; } = new();
        public TextField EmploymentType { get; set; } = new();
        public TextField ExperienceLevel { get; set; } = new();
        public TextField SalaryRange { get; set; } = new();
        public DateTimeField PostingDate { get; set; } = new();
        public DateTimeField ApplicationDeadline { get; set; } = new();
        public BooleanField IsActive { get; set; } = new();
        public BooleanField IsFeatured { get; set; } = new();
        public NumericField ApplicationCount { get; set; } = new();
        public TextField ContactEmail { get; set; } = new();
        public TextField ContactPhone { get; set; } = new();
        public TextField HiringManager { get; set; } = new();
        public TextField JobCategory { get; set; } = new();
        public TextField Priority { get; set; } = new();
        public HtmlField AdditionalInfo { get; set; } = new();
    }
}