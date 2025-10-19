using System.ComponentModel.DataAnnotations;

namespace HoangNgoc.Core.Models;

/// <summary>
/// Payment request model
/// </summary>
public class PaymentRequestModel
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    [Range(1000, 50000000)] // 1,000 VND to 50,000,000 VND
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    
    [Required]
    public string Currency { get; set; } = "VND";
    
    [Required]
    public string ReturnUrl { get; set; }
    
    public string CancelUrl { get; set; }
    
    public string NotifyUrl { get; set; }
    
    public string OrderId { get; set; }
    
    public Dictionary<string, string> ExtraData { get; set; } = new();
}

/// <summary>
/// Payment request result
/// </summary>
public class PaymentRequest
{
    public string TransactionId { get; set; }
    public string PaymentUrl { get; set; }
    public string QrCode { get; set; }
    public DateTime ExpiresAt { get; set; }
    public Dictionary<string, string> AdditionalData { get; set; } = new();
}

/// <summary>
/// Payment processing result
/// </summary>
public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string TransactionId { get; set; }
    public string ExternalTransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public string Message { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    public Dictionary<string, string> AdditionalData { get; set; } = new();
}

/// <summary>
/// Refund result
/// </summary>
public class RefundResult
{
    public bool IsSuccess { get; set; }
    public string RefundId { get; set; }
    public decimal RefundAmount { get; set; }
    public string Message { get; set; }
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Payment status enumeration
/// </summary>
public enum PaymentStatus
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Cancelled = 5,
    Expired = 6,
    Refunded = 7,
    PartiallyRefunded = 8
}