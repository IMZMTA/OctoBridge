using OctoBridge.Domain.Constants;

namespace OctoBridge.Domain.Config;

public class AppSettings
{
    public bool SwaggerEnabled { get; init; }
    public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
    public JwtSettings Jwt { get; init; } = new();
    public EncryptionSettings Encryption { get; init; } = new();
    public ProviderSettings Providers { get; init; } = new();
}

public class JwtSettings
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpirationMinutes { get; init; } = AppConstants.DefaultJwtExpirationMinutes;
    public int ExpirationHours { get; init; } = AppConstants.DefaultJwtExpirationHours;
    public int ExpirationDays { get; init; } = AppConstants.DefaultJwtExpirationDays;
}

public class EncryptionSettings
{
    public const string SectionName = "AppSettings:Encryption";
    public string Key { get; set; } = default!;
    public string IV { get; set; } = default!;
}

public class ProviderSettings
{
    public GitHubSettings GitHub { get; init; } = new();

}

public class GitHubSettings
{
    public const string SectionName = "AppSettings:Providers:GitHub";
    public string ApiBaseUrl { get; init; } = default!;
    public string GitHubPAT { get; init; } = default!;
    public string UserAgent { get; init; } = default!;
    public int TimeoutSeconds { get; init; } = 30;
    public OAuthProviderSettings OAuth { get; init; } = new();
}

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

