namespace OctoBridge.Domain.Config;

public class GitHubSettings
{
    public const string SectionName = "AppSettings:Providers:GitHub";

    public string ApiBaseUrl { get; init; } = default!;

    public string GitHubPAT { get; init; } = default!;

    public string UserAgent { get; init; } = default!;

    public int TimeoutSeconds { get; init; } = 30;

    public OAuthProviderSettings OAuth { get; init; } = new();
}