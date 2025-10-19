using OrchardCore.ContentManagement;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.ViewModels
{
    public class JobPostingPartViewModel
    {
        public JobPostingPart JobPostingPart { get; set; } = new();
        public ContentItem ContentItem { get; set; } = new();
        
        // Form properties for easy binding
        public string JobId { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = "Full-time";
        public string ExperienceLevel { get; set; } = "Mid-level";
        public string SalaryRange { get; set; } = string.Empty;
        public DateTime? PostingDate { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }
        public decimal ApplicationCount { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string HiringManager { get; set; } = string.Empty;
        public string JobCategory { get; set; } = string.Empty;
        public string Priority { get; set; } = "Medium";
        public string AdditionalInfo { get; set; } = string.Empty;
    }
}