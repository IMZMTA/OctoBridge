using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Infrastructure.Clients.OAuthClient;

public interface IGitHubOAuthClient
{
    Task<GitHubTokenModel> ExchangeCodeForTokenAsync(TokenRequestModel request, CancellationToken cancellationToken);
}