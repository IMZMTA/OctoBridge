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
using OctoBridge.Domain.Constants.Messages;

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
        using var request = new HttpRequestMessage(method, url)
        {
            Content = content
        };

        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue(GitHubConstants.MediaType));

        logger.LogInformation("GitHub OAuth → {Method} {Url}", method, url);

        using var response = await httpClient.SendAsync(request, cancellationToken);

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<GitHubOAuthError>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                        ?? new GitHubOAuthError { ErrorDescription = json };
            
            var mappedErrors = GitHubErrorMapper.MapOAuthError(error);

            logger.LogError("GitHub OAuth Error | Status: {StatusCode} | Message: {Message} | Details: {@Errors} | ErrorUri: {Uri}",
                response.StatusCode, error.ErrorDescription, error.Error, error.ErrorUri);

            throw new ExternalApiException(
                ErrorMessages.OAuthErrorOccurred,
                mappedErrors,
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

        using var content = new FormUrlEncodedContent(formData);

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