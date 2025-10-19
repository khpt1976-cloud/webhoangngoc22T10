using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using YesSql;
using HoangNgoc.Application.Models;
using HoangNgoc.Application.Indexes;

namespace HoangNgoc.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IContentManager _contentManager;
        private readonly YesSql.ISession _session;

        public ApplicationService(IContentManager contentManager, YesSql.ISession session)
        {
            _contentManager = contentManager;
            _session = session;
        }

        public async Task<IEnumerable<ContentItem>> GetJobApplicationsAsync()
        {
            return await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobApplication" && x.Published)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetJobPostingsAsync()
        {
            return await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "JobPosting" && x.Published)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetCandidatesAsync()
        {
            return await _session.Query<ContentItem, ContentItemIndex>()
                .Where(x => x.ContentType == "Candidate" && x.Published)
                .ListAsync();
        }

        public async Task<ContentItem?> GetJobApplicationByIdAsync(string applicationId)
        {
            return await _session.Query<ContentItem, JobApplicationIndex>()
                .Where(x => x.ApplicationId == applicationId)
                .FirstOrDefaultAsync();
        }

        public async Task<ContentItem?> GetJobPostingByIdAsync(string jobId)
        {
            return await _session.Query<ContentItem, JobPostingIndex>()
                .Where(x => x.JobId == jobId)
                .FirstOrDefaultAsync();
        }

        public async Task<ContentItem?> GetCandidateByIdAsync(string candidateId)
        {
            return await _session.Query<ContentItem, CandidateIndex>()
                .Where(x => x.CandidateId == candidateId)
                .FirstOrDefaultAsync();
        }

        public async Task<ContentItem> CreateJobApplicationAsync(JobApplicationPart applicationPart)
        {
            var contentItem = await _contentManager.NewAsync("JobApplication");
            contentItem.Weld(applicationPart);
            await _contentManager.CreateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
            return contentItem;
        }

        public async Task<ContentItem> CreateJobPostingAsync(JobPostingPart jobPostingPart)
        {
            var contentItem = await _contentManager.NewAsync("JobPosting");
            contentItem.Weld(jobPostingPart);
            await _contentManager.CreateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
            return contentItem;
        }

        public async Task<ContentItem> CreateCandidateAsync(CandidatePart candidatePart)
        {
            var contentItem = await _contentManager.NewAsync("Candidate");
            contentItem.Weld(candidatePart);
            await _contentManager.CreateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
            return contentItem;
        }

        public async Task UpdateJobApplicationAsync(ContentItem contentItem)
        {
            await _contentManager.UpdateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
        }

        public async Task UpdateJobPostingAsync(ContentItem contentItem)
        {
            await _contentManager.UpdateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
        }

        public async Task UpdateCandidateAsync(ContentItem contentItem)
        {
            await _contentManager.UpdateAsync(contentItem);
            await _contentManager.PublishAsync(contentItem);
        }

        public async Task DeleteJobApplicationAsync(string applicationId)
        {
            var contentItem = await GetJobApplicationByIdAsync(applicationId);
            if (contentItem != null)
            {
                await _contentManager.RemoveAsync(contentItem);
            }
        }

        public async Task DeleteJobPostingAsync(string jobId)
        {
            var contentItem = await GetJobPostingByIdAsync(jobId);
            if (contentItem != null)
            {
                await _contentManager.RemoveAsync(contentItem);
            }
        }

        public async Task DeleteCandidateAsync(string candidateId)
        {
            var contentItem = await GetCandidateByIdAsync(candidateId);
            if (contentItem != null)
            {
                await _contentManager.RemoveAsync(contentItem);
            }
        }

        public async Task<IEnumerable<ContentItem>> SearchJobApplicationsAsync(string searchTerm)
        {
            return await _session.Query<ContentItem, JobApplicationIndex>()
                .Where(x => x.ApplicantName.Contains(searchTerm) || 
                           x.JobTitle.Contains(searchTerm) ||
                           x.ApplicantEmail.Contains(searchTerm))
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> SearchJobPostingsAsync(string searchTerm)
        {
            return await _session.Query<ContentItem, JobPostingIndex>()
                .Where(x => x.JobTitle.Contains(searchTerm) || 
                           x.Department.Contains(searchTerm) ||
                           x.Location.Contains(searchTerm))
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> SearchCandidatesAsync(string searchTerm)
        {
            return await _session.Query<ContentItem, CandidateIndex>()
                .Where(x => x.FullName.Contains(searchTerm) || 
                           x.Email.Contains(searchTerm) ||
                           x.Skills.Contains(searchTerm))
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetActiveJobPostingsAsync()
        {
            return await _session.Query<ContentItem, JobPostingIndex>()
                .Where(x => x.IsActive)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetFeaturedJobPostingsAsync()
        {
            return await _session.Query<ContentItem, JobPostingIndex>()
                .Where(x => x.IsFeatured && x.IsActive)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetApplicationsByJobAsync(string jobId)
        {
            return await _session.Query<ContentItem, JobApplicationIndex>()
                .Where(x => x.JobTitle == jobId)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetApplicationsByStatusAsync(string status)
        {
            return await _session.Query<ContentItem, JobApplicationIndex>()
                .Where(x => x.ApplicationStatus == status)
                .ListAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetAvailableCandidatesAsync()
        {
            return await _session.Query<ContentItem, CandidateIndex>()
                .Where(x => x.IsAvailable)
                .ListAsync();
        }

        public async Task<int> GetApplicationCountForJobAsync(string jobId)
        {
            return await _session.Query<ContentItem, JobApplicationIndex>()
                .Where(x => x.JobTitle == jobId)
                .CountAsync();
        }

        public async Task UpdateApplicationStatusAsync(string applicationId, string status)
        {
            var contentItem = await GetJobApplicationByIdAsync(applicationId);
            if (contentItem != null)
            {
                var applicationPart = contentItem.As<JobApplicationPart>();
                if (applicationPart != null)
                {
                    applicationPart.ApplicationStatus.Text = status;
                    await UpdateJobApplicationAsync(contentItem);
                }
            }
        }

        public async Task MarkCandidateAsHiredAsync(string candidateId, string jobId)
        {
            var candidate = await GetCandidateByIdAsync(candidateId);
            if (candidate != null)
            {
                var candidatePart = candidate.As<CandidatePart>();
                if (candidatePart != null)
                {
                    candidatePart.IsAvailable.Value = false;
                    candidatePart.Notes.Html += $"<p>Hired for job: {jobId} on {DateTime.Now:yyyy-MM-dd}</p>";
                    await UpdateCandidateAsync(candidate);
                }
            }
        }
    }
}