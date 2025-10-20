using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.Indexes
{
    public class CandidateIndex : MapIndex
    {
        public string CandidateId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CurrentPosition { get; set; } = string.Empty;
        public string CurrentCompany { get; set; } = string.Empty;
        public string TotalExperience { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string ExpectedSalary { get; set; } = string.Empty;
        public string PreferredLocation { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string CandidateSource { get; set; } = string.Empty;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string ContentItemId { get; set; } = string.Empty;
    }

    public class CandidateIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<CandidateIndex>()
                .Map(contentItem =>
                {
                    var part = contentItem.As<CandidatePart>();
                    if (part == null) return null;

                    return new CandidateIndex
                    {
                        CandidateId = part.CandidateId.Text ?? string.Empty,
                        FullName = part.FullName.Text ?? string.Empty,
                        Email = part.Email.Text ?? string.Empty,
                        Phone = part.Phone.Text ?? string.Empty,
                        DateOfBirth = part.DateOfBirth.Value,
                        City = part.City.Text ?? string.Empty,
                        Country = part.Country.Text ?? string.Empty,
                        CurrentPosition = part.CurrentPosition.Text ?? string.Empty,
                        CurrentCompany = part.CurrentCompany.Text ?? string.Empty,
                        TotalExperience = part.TotalExperience.Text ?? string.Empty,
                        Skills = part.Skills.Text ?? string.Empty,
                        Education = part.Education.Text ?? string.Empty,
                        ExpectedSalary = part.ExpectedSalary.Text ?? string.Empty,
                        PreferredLocation = part.PreferredLocation.Text ?? string.Empty,
                        IsAvailable = part.IsAvailable.Value,
                        CandidateSource = part.CandidateSource.Text ?? string.Empty,
                        RegistrationDate = part.RegistrationDate.Value,
                        LastUpdated = part.LastUpdated.Value,
                        ContentItemId = contentItem.ContentItemId
                    };
                });
        }
    }
}