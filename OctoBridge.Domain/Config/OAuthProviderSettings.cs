namespace OctoBridge.Domain.Config;

public class OAuthProviderSettings
{
    public string BaseUrl { get; init; } = default!;

    public string AuthorizationUrl { get; init; } = default!;

    public string TokenUrl { get; init; } = default!;

    public string RevokeUrl { get; init; } = default!;

    public string ClientId { get; init; } = default!;

    public string ClientSecret { get; init; } = default!;

    public string RedirectUri { get; init; } = default!;

    public string Scope { get; init; } = default!;
}