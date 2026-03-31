using OctoBridge.Domain.Enums;
using Microsoft.Extensions.Options;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Models;
using OctoBridge.Domain.Constants;
using OctoBridge.Infrastructure.Exceptions;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Infrastructure.Clients.OAuthClient;
using OctoBridge.Infrastructure.Clients.GitHubClient;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.TokenService;

namespace OctoBridge.Infrastructure.Services.OAuthService;

public class GitHubOAuthService : IOAuthService
{
    private readonly OAuthProviderSettings settings;
    private readonly IGitHubOAuthClient oauthClient;
    private readonly IGitHubClient githubApiClient;
    private readonly IUserService userService;
    private readonly ITokenService tokenService;

    public AuthProvider Provider => AuthProvider.GitHub;

    public GitHubOAuthService(IOptions<AppSettings> options, IGitHubOAuthClient _oauthClient, IGitHubClient _githubApiClient, IUserService _userService, ITokenService _tokenService)
    {
        settings = options.Value.Providers.GitHub.OAuth;
        oauthClient = _oauthClient;
        githubApiClient = _githubApiClient;
        userService = _userService;
        tokenService = _tokenService;
    }

    public string GetAuthorizationUrl()
    {
        var baseUrl = settings.BaseUrl;
        var tokenUrl = settings.AuthorizationUrl;
        var clientId = settings.ClientId;
        var redirectUri = settings.RedirectUri;
        var scope = settings.Scope;

        return $"{baseUrl}{tokenUrl}" +
               $"?client_id={clientId}" +
               $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
               $"&scope={Uri.EscapeDataString(scope)}" +
               $"&response_type=code";
    }

    public async Task<TokenModel> GetAccessTokenAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Authorization code cannot be empty.", nameof(code));

        var request = new TokenRequestModel
        {
            ClientId = settings.ClientId,
            ClientSecret = settings.ClientSecret,
            Code = code,
            RedirectUri = settings.RedirectUri
        };

        var tokenResponse = await oauthClient.ExchangeCodeForTokenAsync(request, cancellationToken);

        var githubUser = await githubApiClient.GetUserAsync(tokenResponse.AccessToken, cancellationToken);

        var userModel = new UserModel
        {
            ProviderId = githubUser.Id,
            UserName = githubUser.Login,
            AvatarUrl = githubUser.AvatarUrl,
            ProfileUrl = githubUser.HtmlUrl,
            AccessToken = tokenResponse.AccessToken,
            Email = githubUser.Email ?? string.Empty,
        };

        var savedUser = await userService.UpsertAsync(userModel, cancellationToken) ?? throw new AuthenticationException("Database sync failed.");

        return new TokenModel
        {
            UserId = savedUser.UserId,
            ProviderId = savedUser.ProviderId,
            UserName = savedUser.UserName,
            Email = savedUser.Email,
            TokenType = AppConstants.Bearer,
            IssuedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };

    }

}