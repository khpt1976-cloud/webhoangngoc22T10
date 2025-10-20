using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Payment.Models
{
    public class InvoicePart : ContentPart
    {
        public TextField InvoiceNumber { get; set; } = new();
        public TextField OrderId { get; set; } = new();
        public DateTimeField InvoiceDate { get; set; } = new();
        public DateTimeField DueDate { get; set; } = new();
        public NumericField SubTotal { get; set; } = new();
        public NumericField TaxAmount { get; set; } = new();
        public NumericField DiscountAmount { get; set; } = new();
        public NumericField TotalAmount { get; set; } = new();
        public TextField InvoiceStatus { get; set; } = new();
        public TextField CustomerName { get; set; } = new();
        public TextField CustomerEmail { get; set; } = new();
        public TextField CustomerPhone { get; set; } = new();
        public TextField BillingAddress { get; set; } = new();
        public TextField ShippingAddress { get; set; } = new();
        public TextField PaymentTerms { get; set; } = new();
        public TextField InvoiceNotes { get; set; } = new();
        public BooleanField IsPaid { get; set; } = new();
        public DateTimeField PaidDate { get; set; } = new();
    }
}