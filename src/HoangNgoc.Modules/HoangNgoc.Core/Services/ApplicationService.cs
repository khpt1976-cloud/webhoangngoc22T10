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
using Application = HoangNgoc.Core.Models.Application;

namespace HoangNgoc.Core.Services;

public class ApplicationService : IApplicationService
{
    private readonly YesSql.ISession _session;
    private readonly IWalletService _walletService;
    private readonly ILogger<ApplicationService> _logger;
    private readonly IStringLocalizer S;
    private readonly IMemoryCache _memoryCache;
    private readonly ISignal _signal;

    private const string APPS_CACHE_KEY = "applications_all";
    private const string POPULAR_APPS_CACHE_KEY = "applications_popular";
    private const string CATEGORIES_CACHE_KEY = "application_categories";
    private const string APP_CACHE_TAG = "applications_cache";

    public ApplicationService(
        YesSql.ISession session,
        IWalletService walletService,
        ILogger<ApplicationService> logger,
        IStringLocalizer<ApplicationService> stringLocalizer,
        IMemoryCache memoryCache,
        ISignal signal)
    {
        _session = session;
        _walletService = walletService;
        _logger = logger;
        S = stringLocalizer;
        _memoryCache = memoryCache;
        _signal = signal;
    }

    public async Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetApplicationsAsync(int page = 1, int pageSize = 20, string category = null)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            var filteredApplications = allApplications.Where(a => a.Status == ApplicationStatus.Active);

            if (!string.IsNullOrEmpty(category))
            {
                filteredApplications = filteredApplications.Where(a => a.CategoryId == category);
            }

            return filteredApplications
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting applications");
            return Enumerable.Empty<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<HoangNgoc.Core.Models.Application> GetApplicationAsync(string applicationId)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications.FirstOrDefault(x => x.Id == applicationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting application {ApplicationId}", applicationId);
            return null;
        }
    }

    public async Task<List<HoangNgoc.Core.Models.Application>> GetUserApplicationsAsync(string userId, int page = 1, int pageSize = 10)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications
                .Where(a => a.DeveloperId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user applications for {UserId}", userId);
            return new List<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetPopularApplicationsAsync(int count = 10)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications
                .Where(a => a.Status == ApplicationStatus.Active)
                .OrderByDescending(a => a.UsageCount)
                .Take(count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting popular applications");
            return Enumerable.Empty<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetApplicationsByCategoryAsync(string category, int page = 1, int pageSize = 20)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications
                .Where(a => a.Status == ApplicationStatus.Active && a.CategoryId == category)
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting applications by category {Category}", category);
            return Enumerable.Empty<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<IEnumerable<HoangNgoc.Core.Models.Application>> SearchApplicationsAsync(string query, int page = 1, int pageSize = 20)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications
                .Where(a => a.Status == ApplicationStatus.Active && 
                           (a.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            a.Description.Contains(query, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching applications with query {Query}", query);
            return Enumerable.Empty<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<List<HoangNgoc.Core.Models.Application>> GetFeaturedApplicationsAsync(int count = 6)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            return allApplications
                .Where(a => a.Status == ApplicationStatus.Active && a.IsFeatured)
                .OrderByDescending(a => a.Rating)
                .Take(count)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting featured applications");
            return new List<HoangNgoc.Core.Models.Application>();
        }
    }

    public async Task<HoangNgoc.Core.Models.Application> CreateApplicationAsync(HoangNgoc.Core.Models.Application application)
    {
        try
        {
            application.Id = Guid.NewGuid().ToString();
            application.CreatedAt = DateTime.UtcNow;
            application.UpdatedAt = DateTime.UtcNow;
            application.Status = ApplicationStatus.Draft;

            _session.Save(application);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Created application {ApplicationId}", application.Id);
            return application;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating application");
            return null;
        }
    }

    public async Task<HoangNgoc.Core.Models.Application> UpdateApplicationAsync(HoangNgoc.Core.Models.Application application)
    {
        try
        {
            application.UpdatedAt = DateTime.UtcNow;
            _session.Save(application);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Updated application {ApplicationId}", application.Id);
            return application;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating application {ApplicationId}", application.Id);
            return null;
        }
    }

    public async Task<bool> DeleteApplicationAsync(string applicationId)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            var application = allApplications.FirstOrDefault(x => x.Id == applicationId);
            
            if (application == null)
            {
                _logger.LogWarning("Application {ApplicationId} not found for deletion", applicationId);
                return false;
            }

            _session.Delete(application);
            await _session.SaveChangesAsync();

            _logger.LogInformation("Deleted application {ApplicationId}", applicationId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting application {ApplicationId}", applicationId);
            return false;
        }
    }

    public async Task<ApplicationUsageResult> UseApplicationAsync(string userId, string applicationId)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            var application = allApplications.FirstOrDefault(x => x.Id == applicationId);
            
            if (application == null)
            {
                return new ApplicationUsageResult
                {
                    Success = false,
                    Message = "Application not found"
                };
            }

            if (application.Status != ApplicationStatus.Active)
            {
                return new ApplicationUsageResult
                {
                    Success = false,
                    Message = "Application is not active"
                };
            }

            // Check if user has enough balance
            if (application.Price > 0)
            {
                var balance = await _walletService.GetBalanceAsync(userId);
                if (balance < application.Price)
                {
                    return new ApplicationUsageResult
                    {
                        Success = false,
                        Message = "Insufficient balance"
                    };
                }

                // Deduct balance
                var deductResult = await _walletService.DeductBalanceAsync(userId, application.Price, $"Used application: {application.Name}");
                if (!deductResult)
                {
                    return new ApplicationUsageResult
                    {
                        Success = false,
                        Message = "Failed to deduct balance"
                    };
                }
            }

            // Record usage
            var usage = new ApplicationUsage
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ApplicationId = applicationId,
                Application = application,
                UsedAt = DateTime.UtcNow,
                Parameters = new Dictionary<string, object>()
            };

            _session.Save(usage);
            await _session.SaveChangesAsync();

            // Generate usage session
            var sessionId = Guid.NewGuid().ToString();
            var redirectUrl = $"{application.Url}?session={sessionId}&user={userId}";

            return new ApplicationUsageResult
            {
                Success = true,
                Message = "Application launched successfully",
                UsageId = usage.Id,
                RedirectUrl = redirectUrl
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error using application {ApplicationId} for user {UserId}", applicationId, userId);
            return new ApplicationUsageResult
            {
                Success = false,
                Message = "An error occurred while launching the application"
            };
        }
    }

    public async Task<IEnumerable<ApplicationUsage>> GetUsageHistoryAsync(string userId, int page = 1, int pageSize = 20)
    {
        try
        {
            var skip = (page - 1) * pageSize;
            var allUsages = await _session.Query<ApplicationUsage>().ListAsync();
            return allUsages
                .Where(u => u.UserId == userId)
                .OrderByDescending(u => u.UsedAt)
                .Skip(skip)
                .Take(pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting usage history for user {UserId}", userId);
            return Enumerable.Empty<ApplicationUsage>();
        }
    }

    public async Task<IEnumerable<ApplicationCategory>> GetCategoriesAsync()
    {
        try
        {
            var allCategories = await _session.Query<ApplicationCategory>().ListAsync();
            return allCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting application categories");
            return Enumerable.Empty<ApplicationCategory>();
        }
    }

    public async Task<bool> CanUseApplicationAsync(string userId, string applicationId)
    {
        try
        {
            var allApplications = await _session.Query<HoangNgoc.Core.Models.Application>().ListAsync();
            var application = allApplications.FirstOrDefault(x => x.Id == applicationId);
            
            if (application == null)
            {
                _logger.LogWarning("Application {ApplicationId} not found", applicationId);
                return false;
            }

            if (application.Status != ApplicationStatus.Active)
            {
                _logger.LogInformation("Application {ApplicationId} is not active", applicationId);
                return false;
            }

            // Check if user has sufficient balance for paid applications
            if (application.Price > 0)
            {
                var balance = await _walletService.GetBalanceAsync(userId);
                if (balance < application.Price)
                {
                    _logger.LogInformation("User {UserId} has insufficient balance for application {ApplicationId}. Required: {Price}, Available: {Balance}", 
                        userId, applicationId, application.Price, balance);
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {UserId} can use application {ApplicationId}", userId, applicationId);
            return false;
        }
    }
}