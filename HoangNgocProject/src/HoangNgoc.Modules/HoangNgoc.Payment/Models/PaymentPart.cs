using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Payment.Models
{
    public class PaymentPart : ContentPart
    {
        public TextField PaymentId { get; set; } = new();
        public TextField OrderId { get; set; } = new();
        public NumericField Amount { get; set; } = new();
        public TextField Currency { get; set; } = new();
        public TextField PaymentMethod { get; set; } = new();
        public TextField PaymentStatus { get; set; } = new();
        public TextField TransactionId { get; set; } = new();
        public TextField PaymentGateway { get; set; } = new();
        public DateTimeField PaymentDate { get; set; } = new();
        public TextField CustomerEmail { get; set; } = new();
        public TextField CustomerPhone { get; set; } = new();
        public TextField BillingAddress { get; set; } = new();
        public TextField PaymentDescription { get; set; } = new();
        public TextField PaymentNotes { get; set; } = new();
        public BooleanField IsRefunded { get; set; } = new();
        public NumericField RefundAmount { get; set; } = new();
        public DateTimeField RefundDate { get; set; } = new();
    }
}