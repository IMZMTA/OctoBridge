using MediatR;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.External;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

public class GetRepositoryIssuesListRequestDto : IRequest<ApiResponse<GetRepositoryIssuesListResponseDto>>
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public IssueState State { get; set; } = IssueState.Open;
    public List<string>? Labels { get; set; }
    public string? Assignee { get; set; }
    public int PageSize { get; set; } = GitHubConstants.DefaultPageSize;
    public int PageNo { get; set; } = GitHubConstants.DefaultPage;
    public IssueSortType Sort { get; set; } = IssueSortType.Created;
    public SortDirection Direction { get; set; } = SortDirection.Desc;
    public DateTime? Since { get; set; }

}
