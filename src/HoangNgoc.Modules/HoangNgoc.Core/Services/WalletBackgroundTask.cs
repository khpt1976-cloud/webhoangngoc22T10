using HoangNgoc.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.BackgroundTasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HoangNgoc.Core.Services;

/// <summary>
/// Orchard Core Background Task for processing wallet transactions
/// </summary>
[BackgroundTask(Schedule = "*/5 * * * *", Description = "Process wallet transactions every 5 minutes")]
public class WalletBackgroundTask : IBackgroundTask
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WalletBackgroundTask> _logger;

    public WalletBackgroundTask(
        IServiceProvider serviceProvider,
        ILogger<WalletBackgroundTask> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting wallet transaction processing background task");

            var processor = serviceProvider.GetRequiredService<IWalletTransactionProcessor>();

            // Process pending transactions
            await processor.ProcessPendingTransactionsAsync();

            // Process expired transactions
            await processor.ProcessExpiredTransactionsAsync();

            // Reconcile transactions (run less frequently)
            var currentMinute = DateTime.UtcNow.Minute;
            if (currentMinute % 15 == 0) // Every 15 minutes
            {
                await processor.ReconcileTransactionsAsync();
            }

            _logger.LogInformation("Completed wallet transaction processing background task");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in wallet transaction processing background task");
        }
    }
}