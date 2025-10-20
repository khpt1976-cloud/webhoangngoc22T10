using OrchardCore.ContentManagement;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.ViewModels
{
    public class CandidatePartViewModel
    {
        public CandidatePart CandidatePart { get; set; } = new();
        public ContentItem ContentItem { get; set; } = new();
        
        // Form properties for easy binding
        public string CandidateId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public string CurrentPosition { get; set; } = string.Empty;
        public string CurrentCompany { get; set; } = string.Empty;
        public string TotalExperience { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Certifications { get; set; } = string.Empty;
        public string Languages { get; set; } = string.Empty;
        public string ExpectedSalary { get; set; } = string.Empty;
        public string NoticePeriod { get; set; } = string.Empty;
        public string PreferredLocation { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public string CandidateSource { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}