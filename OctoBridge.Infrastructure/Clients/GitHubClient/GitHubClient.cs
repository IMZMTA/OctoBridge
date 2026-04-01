using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using OctoBridge.Domain.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Common.Helpers;
using OctoBridge.Infrastructure.Exceptions;
using OctoBridge.Domain.Constants.External;
using OctoBridge.Domain.Models.OctoBridgeApp;
using Microsoft.AspNetCore.Components.Forms;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Constants;

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

    private async Task<GitHubApiResponse<T>> SendAsync<T>(HttpMethod method, String url, string accessToken, CancellationToken cancellationToken, object? bodyContent = null, bool includeAcceptHeader = true)
    {
        
        if (string.IsNullOrWhiteSpace(accessToken)){
            logger.LogError("Access token is null or empty.");
            throw new ArgumentException(ErrorMessages.AccessTokenEmpty, nameof(accessToken));
        }

        using var request = new HttpRequestMessage(method, url);

        request.Headers.Authorization = new AuthenticationHeaderValue(SecurityConstants.Bearer, accessToken);

        if (bodyContent != null)
        {
            request.Content = JsonContent.Create(bodyContent);
        }

        if (includeAcceptHeader)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(GitHubConstants.MediaType));
        }

        logger.LogInformation("GitHub API → {Method} {Url}", method, url);

        using var response = await httpClient.SendAsync(request, cancellationToken);
        LogRateLimit(response);

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<GitHubApiError>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                        ?? new GitHubApiError { Message = json };

            var mappedErrors = GitHubErrorMapper.MapToApiErrors(error);

            logger.LogError("GitHub API Error | Status: {StatusCode} | Message: {Message} | Details: {@Errors} | DocumentationUrl: {Url}",
                response.StatusCode, error.Message, error.Errors, error.DocumentationUrl);
            throw new ExternalApiException(
                error.Message,
                mappedErrors,
                response.StatusCode);
        }

        var data = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var apiResponse = new GitHubApiResponse<T>
        {
            Data = data,
            LinkHeader = ExtractLinkHeader(response)
        };

        return apiResponse;
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
        response.Headers.TryGetValues(GitHubConstants.LinkHeader, out var values);
        return values?.FirstOrDefault();
    }

    public async Task<GitHubUserModel> GetUserAsync(string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync<GitHubUserModel>(HttpMethod.Get, GitHubEndpoints.UserProfile, accessToken, cancellationToken);

        return response.Data ?? throw new InvalidOperationException("GitHub API returned an empty user profile.");
    }

    public async Task<RepositoriesResultModel<GitHubRepositoryModel>> GetRepositoriesAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync<IEnumerable<GitHubRepositoryModel>>(HttpMethod.Get, url, accessToken, cancellationToken);

        return new RepositoriesResultModel<GitHubRepositoryModel>
        {
            List = response.Data ?? Enumerable.Empty<GitHubRepositoryModel>(),
            LinkHeader = response.LinkHeader
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

        var response = await SendAsync<GitHubIssueModel>(HttpMethod.Post, url, accessToken, cancellationToken, payload);

        return response.Data ?? throw new InvalidOperationException("GitHub API returned an empty issue response.");
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


        var response = await SendAsync<GitHubPullRequestModel>(HttpMethod.Post, url, accessToken, cancellationToken, payload);

        return response.Data ?? throw new InvalidOperationException("GitHub API returned an empty pull request response.");
    }

    public async Task<RepositoriesResultModel<GitHubIssueModel>> GetRepositoryIssuesAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync<IEnumerable<GitHubIssueModel>>(HttpMethod.Get, url, accessToken, cancellationToken);

        return new RepositoriesResultModel<GitHubIssueModel>
        {
            List = response.Data ?? Enumerable.Empty<GitHubIssueModel>(),
            LinkHeader = response.LinkHeader
        };

    }

    public async Task<RepositoriesResultModel<GitHubCommitModel>> GetCommitsAsync(string url, string accessToken, CancellationToken cancellationToken)
    {

        var response = await SendAsync<IEnumerable<GitHubCommitModel>>(HttpMethod.Get, url, accessToken, cancellationToken);

        return new RepositoriesResultModel<GitHubCommitModel>
        {
            List = response.Data ?? Enumerable.Empty<GitHubCommitModel>(),
            LinkHeader = response.LinkHeader
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
