using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Infrastructure.Clients.GitHubClient;

public interface IGitHubClient
{
    Task<Domain.Models.GitHub.GitHubUserModel> GetUserAsync(string accessToken, CancellationToken cancellationToken);
    Task<bool> RevokeTokenAsync(string accessToken, CancellationToken cancellationToken);
    Task<RepositoriesResultModel<GitHubRepositoryModel>> GetRepositoriesAsync(string url, string accessToken, CancellationToken cancellationToken);
    Task<RepositoriesResultModel<GitHubIssueModel>> GetRepositoryIssuesAsync(string url, string accessToken, CancellationToken cancellationToken);
    Task<GitHubIssueModel> CreateIssueAsync(IssueRequestModel request, string url, string accessToken, CancellationToken cancellationToken);
    Task<GitHubPullRequestModel> CreatePullRequestAsync(PullRequestModel request, string url, string accessToken, CancellationToken cancellationToken);
    Task<RepositoriesResultModel<GitHubCommitModel>> GetCommitsAsync(string url, string accessToken, CancellationToken cancellationToken);
}
