using System.ComponentModel.DataAnnotations;

namespace HoangNgocCMS.Web.ViewModels
{
    public class JobListViewModel
    {
        public List<JobPostingViewModel> Jobs { get; set; } = new();
        public int TotalJobs { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public string? JobType { get; set; }
        public string? ExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool? IsRemote { get; set; }
        public string SortBy { get; set; } = "PostedDate";
        public string SortOrder { get; set; } = "desc";
        
        // Additional properties for controller compatibility
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public bool HasMoreJobs { get; set; }
        
        // Missing properties for JobController
        public string? SearchQuery { get; set; }
        public List<string> JobTypes { get; set; } = new();
        public List<string> ExperienceLevels { get; set; } = new();
    }

    public class JobPostingViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public string ExperienceLevel { get; set; } = string.Empty;
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string SalaryType { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public bool IsRemote { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public DateTime? StartDate { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string CompanyWebsite { get; set; } = string.Empty;
        public string CompanyLogo { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public int ApplicationCount { get; set; }
        public bool IsActive { get; set; }
        public string PostedBy { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public DateTime LastModified { get; set; }
        
        // Missing properties for JobController
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool Published { get; set; }
    }

    public class JobApplicationViewModel
    {
        public string JobId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Job posting ID is required")]
        public string JobPostingId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string ApplicantName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ApplicantEmail { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? ApplicantPhone { get; set; }
        
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CurrentPosition { get; set; } = string.Empty;
        public string CurrentCompany { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string Education { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string WorkExperience { get; set; } = string.Empty;
        public IFormFile? CVFile { get; set; }
        public IFormFile? PortfolioFile { get; set; }
        public bool AgreeTerms { get; set; }
        public bool AllowContact { get; set; }

        [Required(ErrorMessage = "Cover letter is required")]
        [StringLength(2000, ErrorMessage = "Cover letter cannot exceed 2000 characters")]
        public string CoverLetter { get; set; } = string.Empty;

        public IFormFile? Resume { get; set; }

        public string? LinkedInProfile { get; set; }
        public string? PortfolioUrl { get; set; }
        public string? AdditionalInfo { get; set; }

        // Job details for display
        public string JobTitle { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class JobSearchViewModel
    {
        public string? Keywords { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public string? JobType { get; set; }
        public string? ExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public bool? IsRemote { get; set; }
        public DateTime? PostedSince { get; set; }
        public List<string> Skills { get; set; } = new();
        public string SortBy { get; set; } = "PostedDate";
        public string SortOrder { get; set; } = "desc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class JobDashboardViewModel
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int TotalApplications { get; set; }
        public int NewApplications { get; set; }
        public List<JobPostingViewModel> RecentJobs { get; set; } = new();
        public List<JobApplicationSummaryViewModel> RecentApplications { get; set; } = new();
    }

    public class JobApplicationSummaryViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string ResumeUrl { get; set; } = string.Empty;
    }

    public class JobDetailsViewModel
    {
        public JobPostingViewModel Job { get; set; } = new();
        public bool IsUserApplied { get; set; }
        public bool IsJobSaved { get; set; }
        public List<JobPostingViewModel> SimilarJobs { get; set; } = new();
        public List<string> RequiredSkills { get; set; } = new();
        public string CompanyDescription { get; set; } = string.Empty;
        
        // Missing properties for JobController
        public List<JobPostingViewModel> RelatedJobs { get; set; } = new();
        public bool IsUserLoggedIn { get; set; }
        public bool HasUserApplied { get; set; }
    }

    public class ApplicationSuccessViewModel
    {
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ApplicationId { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        
        // Missing property for JobController
        public DateTime SubmittedDate { get; set; }
    }

    public class JobApplicationCreateModel
    {
        public string JobId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        // Properties needed by service
        public string ApplicantName => $"{FirstName} {LastName}".Trim();
        public string ApplicantEmail => Email;
        public string ApplicantPhone => Phone;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CurrentPosition { get; set; } = string.Empty;
        public string CurrentCompany { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string Education { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string WorkExperience { get; set; } = string.Empty;
        public string CVFilePath { get; set; } = string.Empty;
        public string PortfolioFilePath { get; set; } = string.Empty;
        
        // Additional properties needed by services
        public string CoverLetter { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public IFormFile? CVFile { get; set; }
        public IFormFile? PortfolioFile { get; set; }
        public bool AgreeTerms { get; set; }
        public bool AllowContact { get; set; }
    }
}