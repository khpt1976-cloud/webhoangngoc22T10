using OrchardCore.ContentManagement;
using OrchardCore.ContentFields.Fields;

namespace HoangNgoc.Payment.Models
{
    public class OrderPart : ContentPart
    {
        public TextField OrderNumber { get; set; } = new();
        public DateTimeField OrderDate { get; set; } = new();
        public TextField OrderStatus { get; set; } = new();
        public NumericField SubTotal { get; set; } = new();
        public NumericField ShippingCost { get; set; } = new();
        public NumericField TaxAmount { get; set; } = new();
        public NumericField DiscountAmount { get; set; } = new();
        public NumericField TotalAmount { get; set; } = new();
        public TextField Currency { get; set; } = new();
        public TextField CustomerName { get; set; } = new();
        public TextField CustomerEmail { get; set; } = new();
        public TextField CustomerPhone { get; set; } = new();
        public TextField BillingAddress { get; set; } = new();
        public TextField ShippingAddress { get; set; } = new();
        public TextField ShippingMethod { get; set; } = new();
        public TextField PaymentMethod { get; set; } = new();
        public TextField OrderNotes { get; set; } = new();
        public BooleanField IsShipped { get; set; } = new();
        public DateTimeField ShippedDate { get; set; } = new();
        public TextField TrackingNumber { get; set; } = new();
    }
}