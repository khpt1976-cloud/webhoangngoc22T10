using OrchardCore.ContentManagement;
using HoangNgoc.Application.Models;

namespace HoangNgoc.Application.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ContentItem>> GetJobApplicationsAsync();
        Task<IEnumerable<ContentItem>> GetJobPostingsAsync();
        Task<IEnumerable<ContentItem>> GetCandidatesAsync();
        Task<ContentItem?> GetJobApplicationByIdAsync(string applicationId);
        Task<ContentItem?> GetJobPostingByIdAsync(string jobId);
        Task<ContentItem?> GetCandidateByIdAsync(string candidateId);
        Task<ContentItem> CreateJobApplicationAsync(JobApplicationPart applicationPart);
        Task<ContentItem> CreateJobPostingAsync(JobPostingPart jobPostingPart);
        Task<ContentItem> CreateCandidateAsync(CandidatePart candidatePart);
        Task UpdateJobApplicationAsync(ContentItem contentItem);
        Task UpdateJobPostingAsync(ContentItem contentItem);
        Task UpdateCandidateAsync(ContentItem contentItem);
        Task DeleteJobApplicationAsync(string applicationId);
        Task DeleteJobPostingAsync(string jobId);
        Task DeleteCandidateAsync(string candidateId);
        Task<IEnumerable<ContentItem>> SearchJobApplicationsAsync(string searchTerm);
        Task<IEnumerable<ContentItem>> SearchJobPostingsAsync(string searchTerm);
        Task<IEnumerable<ContentItem>> SearchCandidatesAsync(string searchTerm);
        Task<IEnumerable<ContentItem>> GetActiveJobPostingsAsync();
        Task<IEnumerable<ContentItem>> GetFeaturedJobPostingsAsync();
        Task<IEnumerable<ContentItem>> GetApplicationsByJobAsync(string jobId);
        Task<IEnumerable<ContentItem>> GetApplicationsByStatusAsync(string status);
        Task<IEnumerable<ContentItem>> GetAvailableCandidatesAsync();
        Task<int> GetApplicationCountForJobAsync(string jobId);
        Task UpdateApplicationStatusAsync(string applicationId, string status);
        Task MarkCandidateAsHiredAsync(string candidateId, string jobId);
    }
}