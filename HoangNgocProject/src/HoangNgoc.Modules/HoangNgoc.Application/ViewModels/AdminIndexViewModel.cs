using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace HoangNgoc.Application.ViewModels;

public class AdminIndexViewModel
{
    public List<ContentItem> JobPostings { get; set; } = [];
    
    public JobPostingIndexOptions Options { get; set; } = new JobPostingIndexOptions();
    
    [BindNever]
    public dynamic Pager { get; set; }
}

public class JobPostingIndexOptions
{
    public string Search { get; set; }
    
    public JobPostingStatus Status { get; set; }
    
    public JobPostingOrder OrderBy { get; set; }
}

public enum JobPostingStatus
{
    All,
    Published,
    Draft,
    Expired
}

public enum JobPostingOrder
{
    Title,
    CreatedDate,
    ModifiedDate
}