using System.ComponentModel.DataAnnotations;

namespace HoangNgocCMS.Web.ViewModels
{
    public class EventRegistrationModel
    {
        [Required(ErrorMessage = "Event ID is required")]
        public string EventId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string AttendeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string AttendeeEmail { get; set; } = string.Empty;
        
        // Additional properties needed by controllers
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? AttendeePhone { get; set; }
        
        // Alias properties for controller compatibility
        public string Email => AttendeeEmail;
        public string? Phone => AttendeePhone;

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string? Company { get; set; }

        [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
        public string? JobTitle { get; set; }

        [StringLength(500, ErrorMessage = "Special requests cannot exceed 500 characters")]
        public string? SpecialRequests { get; set; }

        [StringLength(500, ErrorMessage = "Dietary requirements cannot exceed 500 characters")]
        public string? DietaryRequirements { get; set; }

        // Alias for controller compatibility
        public string? SpecialRequirements => SpecialRequests;

        public bool AgreeToTerms { get; set; }

        // Event details for display
        public string EventTitle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
    }

    public class EventListViewModel
    {
        public List<EventViewModel> Events { get; set; } = new();
        public int TotalEvents { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public string? EventType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsFree { get; set; }
        public string SortBy { get; set; } = "StartDate";
        public string SortOrder { get; set; } = "asc";
    }

    public class EventViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
        public string OnlineUrl { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Organizer { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string OrganizerEmail { get; set; } = string.Empty;
        public string OrganizerPhone { get; set; } = string.Empty;
        public string OrganizerLogo { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public bool IsFree { get; set; }
        public bool IsFeatured { get; set; }
        public string Banner { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public int MaxAttendees { get; set; }
        public int RegisteredAttendees { get; set; }
        public bool RequiresApproval { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool CanRegister { get; set; }
        public bool IsRegistered { get; set; }
        public int AvailableSpots { get; set; }
    }

    public class EventSearchViewModel
    {
        public string? Keywords { get; set; }
        public string? Category { get; set; }
        public string? EventType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public bool? IsOnline { get; set; }
        public bool? IsFree { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string> Tags { get; set; } = new();
        public string SortBy { get; set; } = "StartDate";
        public string SortOrder { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }

    public class EventDashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int TotalRegistrations { get; set; }
        public int PendingApprovals { get; set; }
        public List<EventViewModel> UpcomingEventsList { get; set; } = new();
        public List<EventRegistrationSummaryViewModel> RecentRegistrations { get; set; } = new();
    }

    public class EventRegistrationSummaryViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
        public string AttendeeName { get; set; } = string.Empty;
        public string AttendeeEmail { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public bool PaymentRequired { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
    }

    public class EventDetailsViewModel
    {
        public EventViewModel Event { get; set; } = new();
        public bool IsUserRegistered { get; set; }
        public int AttendeesCount { get; set; }
        public List<EventViewModel> SimilarEvents { get; set; } = new();
        public List<string> EventTags { get; set; } = new();
        public string OrganizerInfo { get; set; } = string.Empty;
        
        // Additional properties needed by controllers
        public List<EventViewModel> RelatedEvents { get; set; } = new();
        public bool IsUserLoggedIn { get; set; }
        public int MaxAttendees { get; set; }
        public bool IsFull { get; set; }
        public string RegistrationStatus { get; set; } = string.Empty;
    }

    public class EventRegistrationCreateModel
    {
        public string EventId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string SpecialRequirements { get; set; } = string.Empty;
        public string? DietaryRequirements { get; set; }
        public bool AgreeTerms { get; set; }
        public bool AllowMarketing { get; set; }
        
        // Properties needed by service
        public string ParticipantName => $"{FirstName} {LastName}".Trim();
        public string ParticipantEmail => Email;
        public string ParticipantPhone => Phone;
    }

    public class CourseViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public string Prerequisites { get; set; } = string.Empty;
    }

    public class CourseDetailsViewModel
    {
        public CourseViewModel Course { get; set; } = new();
        public bool IsUserEnrolled { get; set; }
        public bool IsInWishlist { get; set; }
        public int EnrolledCount { get; set; }
        public List<string> CourseModules { get; set; } = new();
        
        // Additional properties needed by controllers
        public List<CourseViewModel> RelatedCourses { get; set; } = new();
        public bool IsUserLoggedIn { get; set; }
        public string InstructorInfo { get; set; } = string.Empty;
        public List<string> CourseTags { get; set; } = new();
    }

    public class CourseEnrollmentCreateModel
    {
        public string CourseId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public decimal PaidAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }

    public class NewsArticleDetailsViewModel
    {
        public NewsArticleViewModel Article { get; set; } = new();
        public List<CommentViewModel> Comments { get; set; } = new();
        public List<NewsArticleViewModel> RelatedArticles { get; set; } = new();
        
        // Additional properties needed by controllers
        public int CommentsCount { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int UserRating { get; set; }
        public bool CanComment { get; set; }
        public bool CanRate { get; set; }
    }

    public class JobSearchCriteria
    {
        public string? Query { get; set; }
        public string? Location { get; set; }
        public string? Category { get; set; }
        public string[]? JobTypes { get; set; }
        public string[]? ExperienceLevels { get; set; }
        public string[]? SalaryRanges { get; set; }
        public string[]? Companies { get; set; }
        public string[]? Skills { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
    }

    public class JobApplicationModel
    {
        public string ContentItemId { get; set; } = string.Empty;
        public string DisplayText { get; set; } = string.Empty;
        public dynamic Content { get; set; } = new { };
        public DateTime? PublishedUtc { get; set; }
    }

    public class JobSearchResult
    {
        public List<JobApplicationModel> Jobs { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasMore { get; set; }
    }

    public class CommentCreateModel
    {
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ParentCommentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CommentViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public List<CommentViewModel> Replies { get; set; } = new();
    }

    public class NewsArticleViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        public string ImageUrl { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
    }
}