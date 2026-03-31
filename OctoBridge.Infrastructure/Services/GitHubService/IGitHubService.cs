using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Infrastructure.Services.GitHubService;

public interface IGitHubService
{
    Task<UserResponseModel> GetUserProfileAsync(string accessToken, CancellationToken cancellationToken);
    Task<bool> RevokeTokenAsync(string accessToken, CancellationToken cancellationToken);
    Task<PaginatedResponseModel<RepoResponseModel>> GetRepositoriesAsync(RepoRequestModel request, string accessToken, CancellationToken cancellationToken);
    Task<IssueResponseModel> CreateIssueRepositoryAsync(IssueRequestModel request, string accessToken, CancellationToken cancellationToken);
    Task<PullRequestResponseModel> CreatePullRequestAsync(PullRequestModel request, string accessToken, CancellationToken cancellationToken);
    Task<PaginatedResponseModel<IssueResponseModel>> GetRepositoryIssuesAsync(ListIssuesRequestModel request, string accessToken, CancellationToken cancellationToken);
    Task<PaginatedResponseModel<CommitResponseModel>> GetCommitsAsync(CommitRequestModel request, string accessToken, CancellationToken cancellationToken);
}
