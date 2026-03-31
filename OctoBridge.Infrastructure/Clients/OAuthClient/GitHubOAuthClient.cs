using System.Net.Http.Json;
using System.Net.Http.Headers;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Constants;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Infrastructure.Clients.OAuthClient;

public class GitHubOAuthClient : IGitHubOAuthClient
{
    private readonly HttpClient httpClient;
    private readonly OAuthProviderSettings oAuthProviderSettings;
    private readonly ILogger<GitHubOAuthClient> logger;

    public GitHubOAuthClient(HttpClient _httpClient, IOptions<AppSettings> options, ILogger<GitHubOAuthClient> _logger)
    {
        httpClient = _httpClient;
        oAuthProviderSettings = options.Value.Providers.GitHub.OAuth;
        logger = _logger;
    }

    private async Task<T> SendAsync<T>( HttpMethod method, string url, HttpContent content, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, url)
        {
            Content = content
        };

        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue(GitHubConstants.GitHubApplicationJson));

        logger.LogInformation("GitHub OAuth → {Method} {Url}", method, url);

        var response = await httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);

            logger.LogError("OAuth Error: {Status} {Error}",
                response.StatusCode, error);

            throw new HttpRequestException(
                $"GitHub OAuth Error: {response.StatusCode} - {error}",
                null,
                response.StatusCode);
        }

        var result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);

        return result ?? throw new InvalidOperationException("Empty OAuth response");
    }

    public async Task<GitHubTokenModel> ExchangeCodeForTokenAsync(TokenRequestModel request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var tokenEndpoint = new Uri(new Uri(oAuthProviderSettings.BaseUrl), oAuthProviderSettings.TokenUrl);

        var formData  = new Dictionary<string, string>
        {
            { "client_id", request.ClientId },
            { "client_secret", request.ClientSecret },
            { "code", request.Code },
            { "redirect_uri", request.RedirectUri }
        };

        var content = new FormUrlEncodedContent(formData);

        var response = await SendAsync<GitHubTokenModel>(HttpMethod.Post, tokenEndpoint.ToString(), content, cancellationToken);

        if (!string.IsNullOrWhiteSpace(response.Error))
        {
            logger.LogError("OAuth logical error: {Error} - {Description}",
                response.Error, response.ErrorDescription);

            throw new InvalidOperationException(
                $"GitHub OAuth Error: {response.Error} - {response.ErrorDescription}");
        }

        logger.LogInformation("GitHub OAuth token exchange successful");

        return response;
    }

}