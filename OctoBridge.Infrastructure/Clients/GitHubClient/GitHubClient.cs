using System.Net;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Infrastructure.Clients.GitHubClient;
public class GitHubClient : IGitHubClient
{
    private readonly HttpClient httpClient;
    private readonly OAuthProviderSettings oauthSettings;
    private readonly ILogger<GitHubClient> logger;

    public GitHubClient(HttpClient _httpClient, IOptions<AppSettings> options, ILogger<GitHubClient> _logger)
    {
        logger = _logger;
        httpClient = _httpClient;
        oauthSettings = options.Value.Providers.GitHub.OAuth;
    }

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, String url, string accessToken, CancellationToken cancellationToken, object? bodyContent = null, bool includeAcceptHeader = true)
    {
        var request = new HttpRequestMessage(method, url);
        
        if (string.IsNullOrWhiteSpace(accessToken)){
            logger.LogError("Access token is null or empty.");
            throw new ArgumentException(Messages.AccessTokenEmpty, nameof(accessToken));
        }

        request.Headers.Authorization = new AuthenticationHeaderValue(AppConstants.Bearer, accessToken);

        if (bodyContent != null)
        {
            request.Content = JsonContent.Create(bodyContent);
        }

        if (includeAcceptHeader)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(GitHubConstants.GitHubApplicationJson));
        }

        logger.LogInformation("GitHub API → {Method} {Url}", method, url);

        var response = await httpClient.SendAsync(request, cancellationToken);
        LogRateLimit(response);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);

            logger.LogError("GitHub API Error: {StatusCode}, {Error}", response.StatusCode, error);

            throw new HttpRequestException($"GitHub API Error: {response.StatusCode} - {error}", null, response.StatusCode);
        }

        return response;
    }

    private void LogRateLimit(HttpResponseMessage response)
    {
        if (response.Headers.Contains("X-RateLimit-Remaining"))
        {
            var remaining = response.Headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
            logger.LogInformation("GitHub Rate Limit Remaining: {Remaining}", remaining);
        }
    }

    private static string? ExtractLinkHeader(HttpResponseMessage response)
    {
        response.Headers.TryGetValues(GitHubConstants.Link, out var values);
        return values?.FirstOrDefault();
    }

    public async Task<GitHubUserModel> GetUserAsync(string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync(HttpMethod.Get, GitHubEndpoints.UserProfile, accessToken, cancellationToken);

        return await response.Content.ReadFromJsonAsync<GitHubUserModel>(cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException("GitHub API returned an empty user profile.");
    }

    public async Task<RepositoriesResultModel<GitHubRepositoryModel>> GetRepositoriesAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync(HttpMethod.Get, url, accessToken, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<GitHubRepositoryModel>>(cancellationToken)
               ?? Enumerable.Empty<GitHubRepositoryModel>();

        return new RepositoriesResultModel<GitHubRepositoryModel>
        {
            List = result,
            LinkHeader = ExtractLinkHeader(response)
        };
    }

    public async Task<GitHubIssueModel> CreateIssueAsync(IssueRequestModel issueModel, string url, string accessToken, CancellationToken cancellationToken)
    {
        var payload = new
        {
            title = issueModel.Title,
            body = issueModel.Body,
            assignees = issueModel.Assignees,
            labels = issueModel.Labels,
            type = issueModel.Type
        };

        var response = await SendAsync(HttpMethod.Post, url, accessToken, cancellationToken, payload);

        return await response.Content.ReadFromJsonAsync<GitHubIssueModel>(cancellationToken)
               ?? throw new InvalidOperationException("GitHub API returned an empty issue response.");
    }

    public async Task<GitHubPullRequestModel> CreatePullRequestAsync(PullRequestModel pullRequestModel, string url, string accessToken, CancellationToken cancellationToken)
    {
        var payload = new Dictionary<string, object>
        {
            ["head"] = pullRequestModel.Head,
            ["base"] = pullRequestModel.Base,
            ["body"] = pullRequestModel.Body
                ?? $"Auto-generated PR from '{pullRequestModel.Head}' to '{pullRequestModel.Base}'",
        };

        if (pullRequestModel.Issue.HasValue)
        {
            payload["issue"] = pullRequestModel.Issue.Value;
        }
        else
        {
            payload["title"] = pullRequestModel.Title!;
        }

        if (pullRequestModel.Draft.HasValue)
            payload["draft"] = pullRequestModel.Draft.Value;

        if (pullRequestModel.MaintainerCanModify.HasValue)
            payload["maintainer_can_modify"] = pullRequestModel.MaintainerCanModify.Value;

        if (!string.IsNullOrWhiteSpace(pullRequestModel.HeadRepo))
            payload["head_repo"] = pullRequestModel.HeadRepo;


        var response = await SendAsync(HttpMethod.Post, url, accessToken, cancellationToken, payload);

        return await response.Content.ReadFromJsonAsync<GitHubPullRequestModel>(cancellationToken)
               ?? throw new InvalidOperationException("GitHub API returned an empty pull request response.");
    }

    public async Task<RepositoriesResultModel<GitHubIssueModel>> GetRepositoryIssuesAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync(HttpMethod.Get, url, accessToken, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<GitHubIssueModel>>(cancellationToken)
           ?? Enumerable.Empty<GitHubIssueModel>();

        return new RepositoriesResultModel<GitHubIssueModel>
        {
            List = result,
            LinkHeader = ExtractLinkHeader(response)
        };

    }

    public async Task<RepositoriesResultModel<GitHubCommitModel>> GetCommitsAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync(HttpMethod.Get, url, accessToken, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<GitHubCommitModel>>(cancellationToken)
           ?? Enumerable.Empty<GitHubCommitModel>();

        return new RepositoriesResultModel<GitHubCommitModel>
        {
            List = result,
            LinkHeader = ExtractLinkHeader(response)
        };

    }

    public async Task<bool> RevokeTokenAsync(string accessToken, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            return false;

        var url = string.Format(oauthSettings.RevokeUrl, oauthSettings.ClientId);
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);

        var authString = Convert.ToBase64String(
        Encoding.ASCII.GetBytes($"{oauthSettings.ClientId}:{oauthSettings.ClientSecret}"));

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);

        request.Content = JsonContent.Create(new { access_token = accessToken });

        logger.LogInformation("GitHub OAuth → Revoke Token");

        var response = await httpClient.SendAsync(request, cancellationToken);

        return response.StatusCode == HttpStatusCode.NoContent;

    }


}
