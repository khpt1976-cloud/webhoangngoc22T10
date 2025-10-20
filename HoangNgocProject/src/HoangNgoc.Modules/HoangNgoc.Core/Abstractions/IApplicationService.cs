using HoangNgoc.Core.Models;

namespace HoangNgoc.Core.Abstractions;

/// <summary>
/// Service for managing applications
/// </summary>
public interface IApplicationService
{
    /// <summary>
    /// Get all available applications
    /// </summary>
    Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetApplicationsAsync(int page = 1, int pageSize = 20, string category = null);
    
    /// <summary>
    /// Get application by ID
    /// </summary>
    Task<HoangNgoc.Core.Models.Application> GetApplicationAsync(string applicationId);
    
    /// <summary>
    /// Get popular applications
    /// </summary>
    Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetPopularApplicationsAsync(int count = 10);
    
    /// <summary>
    /// Get applications by category
    /// </summary>
    Task<IEnumerable<HoangNgoc.Core.Models.Application>> GetApplicationsByCategoryAsync(string category, int page = 1, int pageSize = 20);
    
    /// <summary>
    /// Search applications
    /// </summary>
    Task<IEnumerable<HoangNgoc.Core.Models.Application>> SearchApplicationsAsync(string query, int page = 1, int pageSize = 20);
    
    /// <summary>
    /// Use an application (deduct cost from wallet)
    /// </summary>
    Task<ApplicationUsageResult> UseApplicationAsync(string userId, string applicationId);
    
    /// <summary>
    /// Get user's application usage history
    /// </summary>
    Task<IEnumerable<ApplicationUsage>> GetUsageHistoryAsync(string userId, int page = 1, int pageSize = 20);
    
    /// <summary>
    /// Get application categories
    /// </summary>
    Task<IEnumerable<ApplicationCategory>> GetCategoriesAsync();
    
    /// <summary>
    /// Check if user can use application (has sufficient balance)
    /// </summary>
    Task<bool> CanUseApplicationAsync(string userId, string applicationId);
}