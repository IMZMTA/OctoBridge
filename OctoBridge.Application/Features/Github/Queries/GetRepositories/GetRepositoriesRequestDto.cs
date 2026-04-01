using MediatR;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.External;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositories;

public class GetRepositoriesRequestDto : IRequest<ApiResponse<GetRepositoriesResponseDto>>
{
    public string? OwnerName { get; set; }
    public RepoOwnerType OwnerType { get; set; } = RepoOwnerType.Authenticated;
    public RepoVisibility? Visibility { get; set; } = RepoVisibility.All;
    public List<RepoAffiliation>? Affiliations { get; set; }
    public RepoType? Type { get; set; }
    public RepoSort Sort { get; set; } = RepoSort.Updated;
    public SortDirection Direction { get; set; } = SortDirection.Desc;
    public int PageSize { get; set; } = GitHubConstants.DefaultPageSize;
    public int Page { get; set; } = GitHubConstants.DefaultPage;
    public DateTime? Since { get; set; }

}
