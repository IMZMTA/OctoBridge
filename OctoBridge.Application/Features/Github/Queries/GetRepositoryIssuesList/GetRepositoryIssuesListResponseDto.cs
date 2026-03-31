using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

public class GetRepositoryIssuesListResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public List<IssueResponseModel> Issues { get; set; } = new();
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
}
