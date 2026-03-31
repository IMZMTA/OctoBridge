using MediatR;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.Github.Queries.GetCommits;

public class GetCommitsRequestDto : IRequest<ApiResponse<GetCommitsResponseDto>>
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string? Sha { get; set; }
    public string? Path { get; set; }
    public string? Author { get; set; }
    public string? Committer { get; set; }
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
    public int PageSize { get; set; } = GitHubConstants.DefaultPageSize;
    public int PageNo { get; set; } = GitHubConstants.DefaultPage;

}
