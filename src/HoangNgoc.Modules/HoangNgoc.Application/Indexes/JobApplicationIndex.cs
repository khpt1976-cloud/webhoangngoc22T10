using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.Indexes
{
    public class JobApplicationIndex : MapIndex
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string ApplicantPhone { get; set; } = string.Empty;
        public string ApplicationStatus { get; set; } = string.Empty;
        public DateTime? ApplicationDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public bool IsShortlisted { get; set; }
        public bool IsHired { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string ContentItemId { get; set; } = string.Empty;
    }

    public class JobApplicationIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<JobApplicationIndex>()
                .Map(contentItem =>
                {
                    var part = contentItem.As<JobApplicationPart>();
                    if (part == null) return null;

                    return new JobApplicationIndex
                    {
                        ApplicationId = part.ApplicationId.Text ?? string.Empty,
                        JobTitle = part.JobTitle.Text ?? string.Empty,
                        ApplicantName = part.ApplicantName.Text ?? string.Empty,
                        ApplicantEmail = part.ApplicantEmail.Text ?? string.Empty,
                        ApplicantPhone = part.ApplicantPhone.Text ?? string.Empty,
                        ApplicationStatus = part.ApplicationStatus.Text ?? string.Empty,
                        ApplicationDate = part.ApplicationDate.Value,
                        InterviewDate = part.InterviewDate.Value,
                        IsShortlisted = part.IsShortlisted.Value,
                        IsHired = part.IsHired.Value,
                        Department = part.Department.Text ?? string.Empty,
                        Position = part.Position.Text ?? string.Empty,
                        ContentItemId = contentItem.ContentItemId
                    };
                });
        }
    }
}