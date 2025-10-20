using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Training.Models;

namespace HoangNgoc.Training.Indexes
{
    public class EnrollmentIndex : MapIndex
    {
        public string ContentItemId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Progress { get; set; }
        public int FinalScore { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsCertified { get; set; }
        public DateTime? CertificationDate { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public bool Published { get; set; }
    }

    public class EnrollmentIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<EnrollmentIndex>()
                .Map(contentItem =>
                {
                    var enrollmentPart = contentItem.As<EnrollmentPart>();
                    if (enrollmentPart == null) return null;

                    return new EnrollmentIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        CourseId = enrollmentPart.Course?.ContentItemIds?.FirstOrDefault() ?? string.Empty,
                        StudentId = enrollmentPart.StudentId?.Text ?? string.Empty,
                        StudentName = enrollmentPart.StudentName?.Text ?? string.Empty,
                        StudentEmail = enrollmentPart.StudentEmail?.Text ?? string.Empty,
                        EnrollmentDate = enrollmentPart.EnrollmentDate?.Value ?? DateTime.UtcNow,
                        CompletionDate = enrollmentPart.CompletionDate?.Value,
                        Progress = (int)(enrollmentPart.Progress?.Value ?? 0),
                        FinalScore = (int)(enrollmentPart.FinalScore?.Value ?? 0),
                        Status = enrollmentPart.Status?.Text ?? string.Empty,
                        IsCertified = enrollmentPart.IsCertified?.Value ?? false,
                        CertificationDate = enrollmentPart.CertificationDate?.Value,
                        CertificateNumber = enrollmentPart.CertificateNumber?.Text ?? string.Empty,
                        PaymentStatus = enrollmentPart.PaymentStatus?.Text ?? string.Empty,
                        AmountPaid = enrollmentPart.AmountPaid?.Value ?? 0,
                        PaymentDate = enrollmentPart.PaymentDate?.Value,
                        CreatedUtc = contentItem.CreatedUtc ?? DateTime.UtcNow,
                        ModifiedUtc = contentItem.ModifiedUtc ?? DateTime.UtcNow,
                        Published = contentItem.Published
                    };
                });
        }
    }
}