using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.Indexes
{
    public class JobPostingIndex : MapIndex
    {
        public string JobId { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string ExperienceLevel { get; set; } = string.Empty;
        public DateTime? PostingDate { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public decimal ApplicationCount { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string HiringManager { get; set; } = string.Empty;
        public string JobCategory { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string ContentItemId { get; set; } = string.Empty;
    }

    public class JobPostingIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<JobPostingIndex>()
                .Map(contentItem =>
                {
                    var part = contentItem.As<JobPostingPart>();
                    if (part == null) return null;

                    return new JobPostingIndex
                    {
                        JobId = part.JobId.Text ?? string.Empty,
                        JobTitle = part.JobTitle.Text ?? string.Empty,
                        Department = part.Department.Text ?? string.Empty,
                        Location = part.Location.Text ?? string.Empty,
                        EmploymentType = part.EmploymentType.Text ?? string.Empty,
                        ExperienceLevel = part.ExperienceLevel.Text ?? string.Empty,
                        PostingDate = part.PostingDate.Value,
                        ApplicationDeadline = part.ApplicationDeadline.Value,
                        IsActive = part.IsActive.Value,
                        IsFeatured = part.IsFeatured.Value,
                        ApplicationCount = part.ApplicationCount.Value ?? 0,
                        ContactEmail = part.ContactEmail.Text ?? string.Empty,
                        HiringManager = part.HiringManager.Text ?? string.Empty,
                        JobCategory = part.JobCategory.Text ?? string.Empty,
                        Priority = part.Priority.Text ?? string.Empty,
                        ContentItemId = contentItem.ContentItemId
                    };
                });
        }
    }
}