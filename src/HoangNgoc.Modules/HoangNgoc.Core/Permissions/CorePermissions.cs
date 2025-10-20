using OrchardCore.Security.Permissions;

namespace HoangNgoc.Core.Permissions;

/// <summary>
/// Core permissions for HoangNgoc platform
/// </summary>
public class CorePermissions : IPermissionProvider
{
    // Wallet permissions
    public static readonly Permission ManageWallet = new("ManageWallet", "Manage wallet");
    public static readonly Permission ViewWallet = new("ViewWallet", "View wallet");
    public static readonly Permission ManageOwnWallet = new("ManageOwnWallet", "Manage own wallet");
    
    // Application permissions
    public static readonly Permission ManageApplications = new("ManageApplications", "Manage applications");
    public static readonly Permission ViewApplications = new("ViewApplications", "View applications");
    public static readonly Permission UseApplications = new("UseApplications", "Use applications");
    
    // Payment permissions
    public static readonly Permission ManagePayments = new("ManagePayments", "Manage payments");
    public static readonly Permission ViewPayments = new("ViewPayments", "View payments");
    public static readonly Permission ProcessPayments = new("ProcessPayments", "Process payments");
    
    // Training permissions
    public static readonly Permission ManageTraining = new("ManageTraining", "Manage training");
    public static readonly Permission ViewTraining = new("ViewTraining", "View training");
    public static readonly Permission AccessTraining = new("AccessTraining", "Access training");
    
    // Forum permissions
    public static readonly Permission ManageForum = new("ManageForum", "Manage forum");
    public static readonly Permission ViewForum = new("ViewForum", "View forum");
    public static readonly Permission PostInForum = new("PostInForum", "Post in forum");
    public static readonly Permission ModerateForumPosts = new("ModerateForumPosts", "Moderate forum posts");
    
    // Analytics permissions
    public static readonly Permission ViewAnalytics = new("ViewAnalytics", "View analytics");
    public static readonly Permission ManageAnalytics = new("ManageAnalytics", "Manage analytics");

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return Task.FromResult(GetPermissions());
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        return new[]
        {
            new PermissionStereotype
            {
                Name = "Administrator",
                Permissions = GetPermissions()
            },
            new PermissionStereotype
            {
                Name = "Editor",
                Permissions = new[]
                {
                    ManageApplications,
                    ViewApplications,
                    ManageTraining,
                    ViewTraining,
                    ManageForum,
                    ViewForum,
                    PostInForum,
                    ModerateForumPosts,
                    ViewAnalytics
                }
            },
            new PermissionStereotype
            {
                Name = "Moderator",
                Permissions = new[]
                {
                    ViewApplications,
                    ViewTraining,
                    ViewForum,
                    PostInForum,
                    ModerateForumPosts
                }
            },
            new PermissionStereotype
            {
                Name = "Authenticated",
                Permissions = new[]
                {
                    ViewWallet,
                    ManageOwnWallet,
                    ViewApplications,
                    UseApplications,
                    ViewTraining,
                    AccessTraining,
                    ViewForum,
                    PostInForum
                }
            },
            new PermissionStereotype
            {
                Name = "Anonymous",
                Permissions = new[]
                {
                    ViewApplications,
                    ViewTraining,
                    ViewForum
                }
            }
        };
    }

    private static IEnumerable<Permission> GetPermissions()
    {
        return new[]
        {
            // Wallet permissions
            ManageWallet,
            ViewWallet,
            ManageOwnWallet,
            
            // Application permissions
            ManageApplications,
            ViewApplications,
            UseApplications,
            
            // Payment permissions
            ManagePayments,
            ViewPayments,
            ProcessPayments,
            
            // Training permissions
            ManageTraining,
            ViewTraining,
            AccessTraining,
            
            // Forum permissions
            ManageForum,
            ViewForum,
            PostInForum,
            ModerateForumPosts,
            
            // Analytics permissions
            ViewAnalytics,
            ManageAnalytics
        };
    }
}