using HoangNgoc.Core.Abstractions;
using HoangNgoc.Core.Models;
using Microsoft.Extensions.Logging;
using OrchardCore.Data;
using System;
using System.Linq;
using YesSql;

namespace HoangNgoc.Core.Services;

public class WalletTransactionProcessor : IWalletTransactionProcessor
{
    private readonly YesSql.ISession _session;
    private readonly ILogger<WalletTransactionProcessor> _logger;

    public WalletTransactionProcessor(
        YesSql.ISession session,
        ILogger<WalletTransactionProcessor> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task ProcessPendingTransactionsAsync()
    {
        try
        {
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            var pendingTransactions = allTransactions
                .Where(t => t.Status == TransactionStatus.Pending)
                .ToList();

            _logger.LogInformation("Processing {Count} pending transactions", pendingTransactions.Count);

            foreach (var transaction in pendingTransactions)
            {
                try
                {
                    // Check if transaction has been pending for too long (e.g., more than 30 minutes)
                    var pendingDuration = DateTime.UtcNow - transaction.CreatedAt;
                    if (pendingDuration.TotalMinutes > 30)
                    {
                        // Auto-expire old pending transactions
                        transaction.Status = TransactionStatus.Expired;
                        transaction.CompletedAt = DateTime.UtcNow;
                        
                        _session.Save(transaction);
                        _logger.LogInformation("Expired pending transaction {TransactionId} after {Duration} minutes", 
                            transaction.Id, pendingDuration.TotalMinutes);
                    }
                    else
                    {
                        // Here you could check with payment gateway for status updates
                        // For now, we'll just log that we're monitoring it
                        _logger.LogDebug("Monitoring pending transaction {TransactionId}", transaction.Id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing pending transaction {TransactionId}", transaction.Id);
                }
            }

            await _session.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing pending transactions");
        }
    }

    public async Task ProcessExpiredTransactionsAsync()
    {
        try
        {
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            var expiredTransactions = allTransactions
                .Where(t => t.Status == TransactionStatus.Expired)
                .ToList();

            _logger.LogInformation("Processing {Count} expired transactions", expiredTransactions.Count);

            foreach (var transaction in expiredTransactions)
            {
                try
                {
                    // Clean up expired transactions older than 7 days
                    var expiredDuration = DateTime.UtcNow - (transaction.CompletedAt ?? transaction.CreatedAt);
                    if (expiredDuration.TotalDays > 7)
                    {
                        // Mark for cleanup or archive
                        var metadata = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(transaction.Metadata ?? "{}");
                        metadata["ArchivedAt"] = DateTime.UtcNow.ToString("O");
                        transaction.Metadata = System.Text.Json.JsonSerializer.Serialize(metadata);
                        
                        _session.Save(transaction);
                        _logger.LogInformation("Archived expired transaction {TransactionId}", transaction.Id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing expired transaction {TransactionId}", transaction.Id);
                }
            }

            await _session.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing expired transactions");
        }
    }

    public async Task ReconcileTransactionsAsync()
    {
        try
        {
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            var transactionsToReconcile = allTransactions
                .Where(t => t.Status == TransactionStatus.Completed && 
                           (DateTime.UtcNow - t.CreatedAt).TotalHours < 24) // Only reconcile recent transactions
                .ToList();

            _logger.LogInformation("Reconciling {Count} transactions", transactionsToReconcile.Count);

            var reconciliationSummary = new Dictionary<string, int>
            {
                ["TotalProcessed"] = 0,
                ["DiscrepanciesFound"] = 0,
                ["Corrections"] = 0
            };

            foreach (var transaction in transactionsToReconcile)
            {
                try
                {
                    reconciliationSummary["TotalProcessed"]++;

                    // Here you would typically check with external payment gateways
                    // to verify transaction status and amounts
                    
                    // For now, we'll perform basic validation
                    if (transaction.Amount <= 0)
                    {
                        _logger.LogWarning("Transaction {TransactionId} has invalid amount: {Amount}", 
                            transaction.Id, transaction.Amount);
                        reconciliationSummary["DiscrepanciesFound"]++;
                    }

                    if (string.IsNullOrEmpty(transaction.ExternalTransactionId))
                    {
                        _logger.LogWarning("Transaction {TransactionId} missing external transaction ID", 
                            transaction.Id);
                        reconciliationSummary["DiscrepanciesFound"]++;
                    }

                    // Check for duplicate external transaction IDs
                    var duplicates = allTransactions
                        .Where(t => t.ExternalTransactionId == transaction.ExternalTransactionId && t.Id != transaction.Id)
                        .ToList();
                    
                    if (duplicates.Any())
                    {
                        _logger.LogWarning("Duplicate external transaction ID {ExternalId} found for transaction {TransactionId}", 
                            transaction.ExternalTransactionId, transaction.Id);
                        reconciliationSummary["DiscrepanciesFound"]++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error reconciling transaction {TransactionId}", transaction.Id);
                }
            }

            _logger.LogInformation("Reconciliation completed. Processed: {Processed}, Discrepancies: {Discrepancies}, Corrections: {Corrections}",
                reconciliationSummary["TotalProcessed"],
                reconciliationSummary["DiscrepanciesFound"],
                reconciliationSummary["Corrections"]);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during transaction reconciliation");
        }
    }
}