namespace HoangNgoc.Core.Abstractions;

/// <summary>
/// Background service for processing wallet transactions
/// </summary>
public interface IWalletTransactionProcessor
{
    /// <summary>
    /// Process pending transactions
    /// </summary>
    Task ProcessPendingTransactionsAsync();
    
    /// <summary>
    /// Process expired transactions
    /// </summary>
    Task ProcessExpiredTransactionsAsync();
    
    /// <summary>
    /// Reconcile transactions with payment gateways
    /// </summary>
    Task ReconcileTransactionsAsync();
}