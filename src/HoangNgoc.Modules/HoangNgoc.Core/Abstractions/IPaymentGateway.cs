using HoangNgoc.Core.Models;

namespace HoangNgoc.Core.Abstractions;

/// <summary>
/// Interface for payment gateway implementations
/// </summary>
public interface IPaymentGateway
{
    /// <summary>
    /// Gateway name (VNPay, MoMo, ZaloPay, etc.)
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gateway display name
    /// </summary>
    string DisplayName { get; }
    
    /// <summary>
    /// Gateway description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gateway logo URL
    /// </summary>
    string LogoUrl { get; }
    
    /// <summary>
    /// Whether the gateway is enabled
    /// </summary>
    bool IsEnabled { get; }
    
    /// <summary>
    /// Supported currencies
    /// </summary>
    IEnumerable<string> SupportedCurrencies { get; }
    
    /// <summary>
    /// Create a payment request
    /// </summary>
    Task<PaymentRequest> CreatePaymentRequestAsync(PaymentRequestModel model);
    
    /// <summary>
    /// Process payment callback/webhook
    /// </summary>
    Task<PaymentResult> ProcessCallbackAsync(IDictionary<string, string> parameters);
    
    /// <summary>
    /// Query payment status
    /// </summary>
    Task<PaymentStatus> QueryPaymentStatusAsync(string transactionId);
    
    /// <summary>
    /// Refund a payment
    /// </summary>
    Task<RefundResult> RefundPaymentAsync(string transactionId, decimal amount, string reason);
    
    /// <summary>
    /// Validate callback signature
    /// </summary>
    bool ValidateCallback(IDictionary<string, string> parameters);
}