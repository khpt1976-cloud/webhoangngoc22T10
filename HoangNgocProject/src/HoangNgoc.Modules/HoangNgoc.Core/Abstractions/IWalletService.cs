using HoangNgoc.Core.Models;

namespace HoangNgoc.Core.Abstractions;

/// <summary>
/// Service for managing user wallet operations
/// </summary>
public interface IWalletService
{
    /// <summary>
    /// Get wallet balance for a user
    /// </summary>
    Task<decimal> GetBalanceAsync(string userId);
    
    /// <summary>
    /// Add funds to user wallet
    /// </summary>
    Task<bool> AddFundsAsync(string userId, decimal amount, string description, string transactionId = null);
    
    /// <summary>
    /// Deduct funds from user wallet
    /// </summary>
    Task<bool> DeductFundsAsync(string userId, decimal amount, string description, string transactionId = null);
    
    /// <summary>
    /// Get wallet transaction history
    /// </summary>
    Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(string userId, int page = 1, int pageSize = 20);
    
    /// <summary>
    /// Check if user has sufficient balance
    /// </summary>
    Task<bool> HasSufficientBalanceAsync(string userId, decimal amount);
    
    /// <summary>
    /// Deduct balance from user wallet (alias for DeductFundsAsync)
    /// </summary>
    Task<bool> DeductBalanceAsync(string userId, decimal amount, string description);
    
    /// <summary>
    /// Create a pending transaction (for payment processing)
    /// </summary>
    Task<string> CreatePendingTransactionAsync(string userId, decimal amount, string description, string paymentMethod);
    
    /// <summary>
    /// Complete a pending transaction
    /// </summary>
    Task<bool> CompletePendingTransactionAsync(string transactionId, bool success, string externalTransactionId = null);
    
    /// <summary>
    /// Request top-up for user wallet
    /// </summary>
    Task<string> RequestTopUpAsync(string userId, decimal amount, string paymentMethod = "bank_transfer");
}