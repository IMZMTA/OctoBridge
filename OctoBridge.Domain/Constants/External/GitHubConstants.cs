namespace OctoBridge.Domain.Constants.External;

public static class GitHubConstants
{
    public const string MediaType = "application/vnd.github+json";

    public const int MaxPageSize = 100;
    public const int DefaultPageSize = 30;
    public const int DefaultPage = 1;

    public const string DefaultBranch = "main";
    public const string LinkHeader = "Link";
}