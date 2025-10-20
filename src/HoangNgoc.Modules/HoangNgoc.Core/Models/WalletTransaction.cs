using System;
using System.ComponentModel.DataAnnotations;

namespace HoangNgoc.Core.Models;

/// <summary>
/// Represents a wallet transaction
/// </summary>
public class WalletTransaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public TransactionType Type { get; set; }
    
    [Required]
    public TransactionStatus Status { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    
    public string ExternalTransactionId { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public DateTime? ExpiresAt { get; set; }
    
    public string Notes { get; set; }
    
    public string ReferenceId { get; set; }
    
    public string Metadata { get; set; } // JSON for additional data
    
    public decimal BalanceAfter { get; set; }
}