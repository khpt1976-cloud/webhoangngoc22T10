using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Training.Models
{
    public class EnrollmentPart : ContentPart
    {
        public ContentPickerField Course { get; set; } = new();
        public TextField StudentId { get; set; } = new();
        public TextField StudentName { get; set; } = new();
        public TextField StudentEmail { get; set; } = new();
        public DateTimeField EnrollmentDate { get; set; } = new();
        public DateTimeField CompletionDate { get; set; } = new();
        public NumericField Progress { get; set; } = new();
        public NumericField FinalScore { get; set; } = new();
        public TextField Status { get; set; } = new();
        public BooleanField IsCertified { get; set; } = new();
        public DateTimeField CertificationDate { get; set; } = new();
        public TextField CertificateNumber { get; set; } = new();
        public TextField PaymentStatus { get; set; } = new();
        public NumericField AmountPaid { get; set; } = new();
        public DateTimeField PaymentDate { get; set; } = new();
        public TextField Notes { get; set; } = new();
    }
}