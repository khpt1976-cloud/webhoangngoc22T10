using OrchardCore.ContentManagement;
using YesSql.Indexes;
using HoangNgoc.Payment.Models;

namespace HoangNgoc.Payment.Indexes
{
    public class PaymentIndex : MapIndex
    {
        public string ContentItemId { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string PaymentGateway { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public bool IsRefunded { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime? RefundDate { get; set; }
        public bool Published { get; set; }
        public bool Latest { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
    }

    public class PaymentIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<PaymentIndex>()
                .Map(contentItem =>
                {
                    var paymentPart = contentItem.As<PaymentPart>();
                    if (paymentPart == null) return null!;

                    return new PaymentIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        PaymentId = paymentPart.PaymentId?.Text ?? string.Empty,
                        OrderId = paymentPart.OrderId?.Text ?? string.Empty,
                        Amount = paymentPart.Amount?.Value ?? 0,
                        Currency = paymentPart.Currency?.Text ?? string.Empty,
                        PaymentMethod = paymentPart.PaymentMethod?.Text ?? string.Empty,
                        PaymentStatus = paymentPart.PaymentStatus?.Text ?? string.Empty,
                        TransactionId = paymentPart.TransactionId?.Text ?? string.Empty,
                        PaymentGateway = paymentPart.PaymentGateway?.Text ?? string.Empty,
                        PaymentDate = paymentPart.PaymentDate?.Value,
                        CustomerEmail = paymentPart.CustomerEmail?.Text ?? string.Empty,
                        CustomerPhone = paymentPart.CustomerPhone?.Text ?? string.Empty,
                        IsRefunded = paymentPart.IsRefunded?.Value ?? false,
                        RefundAmount = paymentPart.RefundAmount?.Value ?? 0,
                        RefundDate = paymentPart.RefundDate?.Value,
                        Published = contentItem.Published,
                        Latest = contentItem.Latest,
                        CreatedUtc = contentItem.CreatedUtc ?? DateTime.UtcNow,
                        ModifiedUtc = contentItem.ModifiedUtc ?? DateTime.UtcNow
                    };
                });
        }
    }
}