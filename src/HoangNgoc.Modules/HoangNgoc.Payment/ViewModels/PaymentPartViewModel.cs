using OrchardCore.ContentManagement;
using HoangNgoc.Payment.Models;

namespace HoangNgoc.Payment.ViewModels
{
    public class PaymentPartViewModel
    {
        public PaymentPart PaymentPart { get; set; } = new();
        public ContentItem ContentItem { get; set; } = new();
        
        public string PaymentId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending";
        public string TransactionId { get; set; } = string.Empty;
        public string PaymentGateway { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string PaymentDescription { get; set; } = string.Empty;
        public string PaymentNotes { get; set; } = string.Empty;
        public bool IsRefunded { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime? RefundDate { get; set; }
    }
}