namespace HoangNgocCMS.Web.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsPublic { get; set; }
        public string LinkedInProfile { get; set; } = string.Empty;
        public string GitHubProfile { get; set; } = string.Empty;
        public string TwitterProfile { get; set; } = string.Empty;
        public string FacebookProfile { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public decimal? ExpectedSalary { get; set; }
        public string PreferredJobType { get; set; } = string.Empty;
        public string PreferredLocation { get; set; } = string.Empty;
        public bool IsAvailableForWork { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string Languages { get; set; } = string.Empty;
        public string Certifications { get; set; } = string.Empty;
        public string Portfolio { get; set; } = string.Empty;
        public int ProfileViews { get; set; }
        public DateTime? LastLoginDate { get; set; }
        
        // Missing properties for AccountController
        public object User { get; set; } = new();
        public object Profile { get; set; } = new();
        public object Applications { get; set; } = new();
        public object SavedJobs { get; set; } = new();
        public int ApplicationsCount { get; set; }
        public int SavedJobsCount { get; set; }
    }

    public class UserProfileUpdateModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public string LinkedInProfile { get; set; } = string.Empty;
        public string GitHubProfile { get; set; } = string.Empty;
        public string TwitterProfile { get; set; } = string.Empty;
        public string FacebookProfile { get; set; } = string.Empty;
        public decimal? ExpectedSalary { get; set; }
        public string PreferredJobType { get; set; } = string.Empty;
        public string PreferredLocation { get; set; } = string.Empty;
        public bool IsAvailableForWork { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string Languages { get; set; } = string.Empty;
        public string Certifications { get; set; } = string.Empty;
        public string Portfolio { get; set; } = string.Empty;
    }
}