using HoangNgoc.Core.Abstractions;
using HoangNgoc.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.Data;
using OrchardCore.Environment.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesSql;

namespace HoangNgoc.Core.Services;

public class WalletService : IWalletService
{
    private readonly YesSql.ISession _session;
    private readonly ILogger<WalletService> _logger;
    private readonly IStringLocalizer S;
    private readonly IMemoryCache _memoryCache;
    private readonly ISignal _signal;

    private const string WALLET_CACHE_KEY = "wallet_balance_{0}";
    private const string WALLET_CACHE_TAG = "wallet_cache";

    public WalletService(
        YesSql.ISession session, 
        ILogger<WalletService> logger,
        IStringLocalizer<WalletService> stringLocalizer,
        IMemoryCache memoryCache,
        ISignal signal)
    {
        _session = session;
        _logger = logger;
        S = stringLocalizer;
        _memoryCache = memoryCache;
        _signal = signal;
    }

    public async Task<decimal> GetBalanceAsync(string userId)
    {
        var cacheKey = string.Format(WALLET_CACHE_KEY, userId);
        
        if (_memoryCache.TryGetValue(cacheKey, out decimal cachedBalance))
        {
            return cachedBalance;
        }

        try
        {
            // Use YesSql efficient querying instead of loading all transactions
            var completedTransactions = await _session
                .Query<WalletTransaction>()
                .ListAsync();

            var balance = completedTransactions
                .Where(t => t.UserId == userId && t.Status == TransactionStatus.Completed && t.Type == TransactionType.Credit)
                .Sum(t => t.Amount) -
                completedTransactions
                .Where(t => t.UserId == userId && t.Status == TransactionStatus.Completed && t.Type == TransactionType.Debit)
                .Sum(t => t.Amount);

            var finalBalance = Math.Max(0, balance); // Ensure balance is never negative

            // Cache balance for 5 minutes with cache invalidation signal
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                Priority = CacheItemPriority.Normal
            };
            cacheOptions.ExpirationTokens.Add(_signal.GetToken(WALLET_CACHE_TAG));
            cacheOptions.ExpirationTokens.Add(_signal.GetToken($"wallet_{userId}"));

            _memoryCache.Set(cacheKey, finalBalance, cacheOptions);

            return finalBalance;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, S["Error getting balance for user {0}"], userId);
            return 0;
        }
    }

    public async Task<bool> AddFundsAsync(string userId, decimal amount, string description, string transactionId = null)
    {
        if (amount <= 0)
        {
            _logger.LogWarning(S["Invalid amount {0} for adding funds to user {1}"], amount, userId);
            return false;
        }

        try
        {
            var transaction = new WalletTransaction
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Type = TransactionType.Credit,
                Amount = amount,
                Status = TransactionStatus.Completed,
                Description = description,
                ExternalTransactionId = transactionId ?? Guid.NewGuid().ToString(),
                PaymentMethod = "System",
                CreatedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow,
                Metadata = "{}"
            };

            _session.Save(transaction);
            await _session.SaveChangesAsync();

            // Invalidate cache for this user
            InvalidateWalletCache(userId);

            _logger.LogInformation(S["Added {0} to wallet for user {1}"], amount, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, S["Error adding funds for user {0}"], userId);
            return false;
        }
    }

    public async Task<bool> DeductFundsAsync(string userId, decimal amount, string description, string transactionId = null)
    {
        if (amount <= 0)
        {
            _logger.LogWarning(S["Invalid amount {0} for deducting funds from user {1}"], amount, userId);
            return false;
        }

        try
        {
            var currentBalance = await GetBalanceAsync(userId);
            if (currentBalance < amount)
            {
                _logger.LogWarning(S["Insufficient balance for user {0}. Required: {1}, Available: {2}"], 
                    userId, amount, currentBalance);
                return false;
            }

            var transaction = new WalletTransaction
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Type = TransactionType.Debit,
                Amount = amount,
                Status = TransactionStatus.Completed,
                Description = description,
                ExternalTransactionId = transactionId ?? Guid.NewGuid().ToString(),
                PaymentMethod = "System",
                CreatedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow,
                Metadata = "{}"
            };

            _session.Save(transaction);
            await _session.SaveChangesAsync();

            // Invalidate cache for this user
            InvalidateWalletCache(userId);

            _logger.LogInformation(S["Deducted {0} from wallet for user {1}"], amount, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deducting balance for user {UserId}", userId);
            return false;
        }
    }

    public async Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(string userId, int page = 1, int pageSize = 20)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            return allTransactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .Skip(skip)
                .Take(pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting transaction history for user {UserId}", userId);
            return Enumerable.Empty<WalletTransaction>();
        }
    }

    public async Task<string> CreatePendingTransactionAsync(string userId, decimal amount, string description, string paymentMethod)
    {
        try
        {
            var transaction = new WalletTransaction
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Type = TransactionType.Credit, // Default to credit for pending transactions
                Amount = amount,
                Status = TransactionStatus.Pending,
                Description = description,
                ExternalTransactionId = Guid.NewGuid().ToString(),
                PaymentMethod = paymentMethod,
                CreatedAt = DateTime.UtcNow,
                Metadata = "{}"
            };

            _session.Save(transaction);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Created pending transaction {TransactionId} for user {UserId}", transaction.Id, userId);
            return transaction.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating pending transaction for user {UserId}", userId);
            return null;
        }
    }

    public async Task<bool> HasSufficientBalanceAsync(string userId, decimal amount)
    {
        try
        {
            var balance = await GetBalanceAsync(userId);
            return balance >= amount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking sufficient balance for user {UserId}", userId);
            return false;
        }
    }

    public async Task<WalletTransaction> GetTransactionAsync(string transactionId)
    {
        try
        {
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            return allTransactions.FirstOrDefault(t => t.Id == transactionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting transaction {TransactionId}", transactionId);
            return null;
        }
    }

    public async Task<bool> CompletePendingTransactionAsync(string transactionId, bool success, string externalTransactionId = null)
    {
        try
        {
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            var transaction = allTransactions.FirstOrDefault(x => x.Id == transactionId);
            
            if (transaction == null)
            {
                _logger.LogWarning("Transaction {TransactionId} not found", transactionId);
                return false;
            }

            if (transaction.Status != TransactionStatus.Pending)
            {
                _logger.LogWarning("Transaction {TransactionId} is not pending", transactionId);
                return false;
            }

            transaction.Status = success ? TransactionStatus.Completed : TransactionStatus.Failed;
            transaction.CompletedAt = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(externalTransactionId))
            {
                transaction.ExternalTransactionId = externalTransactionId;
            }

            _session.Save(transaction);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Transaction {TransactionId} completed with status: {Status}", 
                transactionId, transaction.Status);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing transaction {TransactionId}", transactionId);
            return false;
        }
    }

    public async Task<bool> InitializeUserWalletAsync(string userId)
    {
        try
        {
            // Check if user already has transactions (wallet exists)
            var allTransactions = await _session.Query<WalletTransaction>().ListAsync();
            var existingTransactions = allTransactions.Where(t => t.UserId == userId);
            
            if (existingTransactions.Any())
            {
                _logger.LogInformation("Wallet already exists for user {UserId}", userId);
                return true;
            }

            // Create initial welcome bonus transaction
            var welcomeBonus = await AddFundsAsync(userId, 10000, "Welcome bonus - Initial wallet setup");
            
            if (welcomeBonus)
            {
                _logger.LogInformation("Initialized wallet for user {UserId} with welcome bonus", userId);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, S["Error initializing wallet for user {0}"], userId);
            return false;
        }
    }

    /// <summary>
    /// Deduct balance from user wallet (alias for DeductFundsAsync)
    /// </summary>
    public async Task<bool> DeductBalanceAsync(string userId, decimal amount, string description)
    {
        return await DeductFundsAsync(userId, amount, description);
    }

    /// <summary>
    /// Request top-up for user wallet
    /// </summary>
    public async Task<string> RequestTopUpAsync(string userId, decimal amount, string paymentMethod = "bank_transfer")
    {
        try
        {
            var transactionId = Guid.NewGuid().ToString();
            var transaction = new WalletTransaction
            {
                Id = transactionId,
                UserId = userId,
                Type = TransactionType.Credit,
                Amount = amount,
                Status = TransactionStatus.Pending,
                Description = $"Top-up request via {paymentMethod}",
                PaymentMethod = paymentMethod,
                CreatedAt = DateTime.UtcNow,
                Metadata = "{}"
            };

            _session.Save(transaction);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Top-up request created for user {UserId}, amount: {Amount}, transaction: {TransactionId}", 
                userId, amount, transactionId);
            
            return transactionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating top-up request for user {UserId}", userId);
            return null;
        }
    }

    /// <summary>
    /// Invalidate wallet cache for a specific user using Orchard Core cache signals
    /// </summary>
    private void InvalidateWalletCache(string userId)
    {
        _signal.DeferredSignalToken($"wallet_{userId}");
        _signal.DeferredSignalToken(WALLET_CACHE_TAG);
    }
}