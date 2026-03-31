using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Models;

namespace OctoBridge.Infrastructure.Services.OAuthService;

public interface IOAuthService
{
    AuthProvider Provider { get; }

    string GetAuthorizationUrl();

    Task<TokenModel> GetAccessTokenAsync(string code, CancellationToken cancellationToken);
}