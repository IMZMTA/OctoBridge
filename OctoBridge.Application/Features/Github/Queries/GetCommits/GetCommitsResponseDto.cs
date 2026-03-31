using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Application.Features.Github.Queries.GetCommits;

public class GetCommitsResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<CommitResponseModel> Commits { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
}
