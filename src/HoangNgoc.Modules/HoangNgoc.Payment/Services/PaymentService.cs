using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using HoangNgoc.Payment.Models;
using HoangNgoc.Payment.Indexes;

namespace HoangNgoc.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IContentManager _contentManager;
        private readonly YesSql.ISession _session;

        public PaymentService(IContentManager contentManager, YesSql.ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public async Task<ContentItem> CreatePaymentAsync(PaymentPart paymentPart)
        {
            var contentItem = await _contentManager.NewAsync("Payment");
            contentItem.Alter<PaymentPart>(part =>
            {
                part.PaymentId = paymentPart.PaymentId;
                part.OrderId = paymentPart.OrderId;
                part.Amount = paymentPart.Amount;
                part.Currency = paymentPart.Currency;
                part.PaymentMethod = paymentPart.PaymentMethod;
                part.PaymentStatus = paymentPart.PaymentStatus;
                part.TransactionId = paymentPart.TransactionId;
                part.PaymentGateway = paymentPart.PaymentGateway;
                part.PaymentDate = paymentPart.PaymentDate;
                part.CustomerEmail = paymentPart.CustomerEmail;
                part.CustomerPhone = paymentPart.CustomerPhone;
                part.BillingAddress = paymentPart.BillingAddress;
                part.PaymentDescription = paymentPart.PaymentDescription;
                part.PaymentNotes = paymentPart.PaymentNotes;
            });

            await _contentManager.CreateAsync(contentItem);
            return contentItem;
        }

        public async Task<ContentItem?> GetPaymentByIdAsync(string paymentId)
        {
            var payments = await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.PaymentId == paymentId)
                .ListAsync();

            return payments.FirstOrDefault();
        }

        public async Task<ContentItem?> GetPaymentByTransactionIdAsync(string transactionId)
        {
            var payments = await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.TransactionId == transactionId)
                .ListAsync();

            return payments.FirstOrDefault();
        }

        public async Task<IEnumerable<ContentItem>> GetPaymentsByOrderIdAsync(string orderId)
        {
            return await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.OrderId == orderId)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetPaymentsByStatusAsync(string status)
        {
            return await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.PaymentStatus == status)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetPaymentsByCustomerEmailAsync(string customerEmail)
        {
            return await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.CustomerEmail == customerEmail)
                .ListAsync();
        }

        public async Task<bool> UpdatePaymentStatusAsync(string paymentId, string status)
        {
            var payment = await GetPaymentByIdAsync(paymentId);
            if (payment == null) return false;

            payment.Alter<PaymentPart>(part => part.PaymentStatus.Text = status);
            await _contentManager.UpdateAsync(payment);
            return true;
        }

        public async Task<bool> ProcessRefundAsync(string paymentId, decimal refundAmount, string reason)
        {
            var payment = await GetPaymentByIdAsync(paymentId);
            if (payment == null) return false;

            payment.Alter<PaymentPart>(part =>
            {
                part.IsRefunded.Value = true;
                part.RefundAmount.Value = refundAmount;
                part.RefundDate.Value = DateTime.UtcNow;
                part.PaymentNotes.Text += $"\nRefund: {refundAmount} - {reason}";
                part.PaymentStatus.Text = "Refunded";
            });

            await _contentManager.UpdateAsync(payment);
            return true;
        }

        public async Task<decimal> GetTotalPaymentAmountAsync(DateTime fromDate, DateTime toDate)
        {
            var payments = await _session.Query<ContentItem, PaymentIndex>()
                .Where(x => x.PaymentDate >= fromDate && x.PaymentDate <= toDate && x.PaymentStatus == "Completed")
                .ListAsync();

            return payments.Sum(p => p.As<PaymentPart>()?.Amount?.Value ?? 0);
        }

        public async Task<IEnumerable<ContentItem>> GetRecentPaymentsAsync(int count = 10)
        {
            return await _session.Query<ContentItem, PaymentIndex>()
                .OrderByDescending(x => x.PaymentDate)
                .Take(count)
                .ListAsync();
        }

        public Task<bool> ValidatePaymentAsync(PaymentPart paymentPart)
        {
            var isValid = !string.IsNullOrEmpty(paymentPart.PaymentId?.Text) &&
                         !string.IsNullOrEmpty(paymentPart.OrderId?.Text) &&
                         paymentPart.Amount?.Value > 0 &&
                         !string.IsNullOrEmpty(paymentPart.Currency?.Text) &&
                         !string.IsNullOrEmpty(paymentPart.PaymentMethod?.Text);

            return Task.FromResult(isValid);
        }
    }
}