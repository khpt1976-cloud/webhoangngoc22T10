using System.ComponentModel.DataAnnotations;

namespace HoangNgoc.Core.Models;

/// <summary>
/// Represents an application in the platform
/// </summary>
public class Application
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(1000)]
    public string Description { get; set; }
    
    public string ShortDescription { get; set; }
    
    [Required]
    public string CategoryId { get; set; }
    
    public ApplicationCategory Category { get; set; }
    
    [Required]
    [Range(0, 1000000)]
    public decimal Price { get; set; } // Price per use in VND
    
    public string IconUrl { get; set; }
    
    public string[] Screenshots { get; set; } = Array.Empty<string>();
    
    [Required]
    public string Url { get; set; } // Application URL
    
    public string Version { get; set; }
    
    public string Developer { get; set; }
    
    public string DeveloperId { get; set; }
    
    public string DeveloperUrl { get; set; }
    
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Active;
    
    public bool IsFeatured { get; set; }
    
    public bool IsPopular { get; set; }
    
    public int UsageCount { get; set; }
    
    public double Rating { get; set; }
    
    public int ReviewCount { get; set; }
    
    public string[] Tags { get; set; } = Array.Empty<string>();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public string Metadata { get; set; } // JSON for additional data
}

/// <summary>
/// Application category
/// </summary>
public class ApplicationCategory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
    
    public string IconUrl { get; set; }
    
    public string Color { get; set; }
    
    public int SortOrder { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Application usage record
/// </summary>
public class ApplicationUsage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public string ApplicationId { get; set; }
    
    public Application Application { get; set; }
    
    [Required]
    public decimal Cost { get; set; }
    
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    
    public string TransactionId { get; set; }
    
    public string SessionId { get; set; }
    
    public TimeSpan? Duration { get; set; }
    
    public string Metadata { get; set; } // JSON for additional data
    
    public Dictionary<string, object> Parameters { get; set; } = new();
}

/// <summary>
/// Application usage result
/// </summary>
public class ApplicationUsageResult
{
    public bool IsSuccess { get; set; }
    public bool Success { get => IsSuccess; set => IsSuccess = value; } // Alias for backward compatibility
    public string Message { get; set; }
    public string UsageId { get; set; }
    public string RedirectUrl { get; set; }
    public decimal Cost { get; set; }
    public decimal RemainingBalance { get; set; }
}

/// <summary>
/// Application status enumeration
/// </summary>
public enum ApplicationStatus
{
    Draft = 1,
    Active = 2,
    Inactive = 3,
    Maintenance = 4,
    Deprecated = 5
}