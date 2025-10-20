using OrchardCore.ContentManagement;
using HoangNgoc.Payment.Models;

namespace HoangNgoc.Payment.Services
{
    public interface IPaymentService
    {
        Task<ContentItem> CreatePaymentAsync(PaymentPart paymentPart);
        Task<ContentItem?> GetPaymentByIdAsync(string paymentId);
        Task<ContentItem?> GetPaymentByTransactionIdAsync(string transactionId);
        Task<IEnumerable<ContentItem>> GetPaymentsByOrderIdAsync(string orderId);
        Task<IEnumerable<ContentItem>> GetPaymentsByStatusAsync(string status);
        Task<IEnumerable<ContentItem>> GetPaymentsByCustomerEmailAsync(string customerEmail);
        Task<bool> UpdatePaymentStatusAsync(string paymentId, string status);
        Task<bool> ProcessRefundAsync(string paymentId, decimal refundAmount, string reason);
        Task<decimal> GetTotalPaymentAmountAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<ContentItem>> GetRecentPaymentsAsync(int count = 10);
        Task<bool> ValidatePaymentAsync(PaymentPart paymentPart);
    }
}