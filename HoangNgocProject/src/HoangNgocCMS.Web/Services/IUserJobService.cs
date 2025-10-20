using HoangNgocCMS.Web.ViewModels;

namespace HoangNgocCMS.Web.Services
{
    public interface IUserJobService
    {
        Task<bool> IsJobSavedAsync(string userId, string jobId);
        Task<bool> SaveJobAsync(string userId, string jobId);
        Task<bool> UnsaveJobAsync(string userId, string jobId);
        Task<List<JobListViewModel>> GetSavedJobsAsync(string userId, int page = 1, int pageSize = 20);
        Task<List<JobListViewModel>> GetAppliedJobsAsync(string userId, int page = 1, int pageSize = 20);
        Task<Dictionary<string, object>> GetUserJobStatsAsync(string userId);
    }

    public class UserJobService : IUserJobService
    {
        public async Task<bool> IsJobSavedAsync(string userId, string jobId)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return false;
        }

        public async Task<bool> SaveJobAsync(string userId, string jobId)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> UnsaveJobAsync(string userId, string jobId)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return true;
        }

        public async Task<List<JobListViewModel>> GetSavedJobsAsync(string userId, int page = 1, int pageSize = 20)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return new List<JobListViewModel>();
        }

        public async Task<List<JobListViewModel>> GetAppliedJobsAsync(string userId, int page = 1, int pageSize = 20)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return new List<JobListViewModel>();
        }

        public async Task<Dictionary<string, object>> GetUserJobStatsAsync(string userId)
        {
            // TODO: Implement with OrchardCore ContentManager
            await Task.CompletedTask;
            return new Dictionary<string, object>();
        }
    }
}