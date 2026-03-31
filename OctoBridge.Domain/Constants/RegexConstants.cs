namespace OctoBridge.Domain.Constants;

public static class RegexConstants
{
    // GitHub branch name (supports "branch" or "user:branch")
    public const string GitHubHeadBranch = @"^[^:\s]+(:[^:\s]+)?$";

    // Simple branch name (no spaces)
    public const string GitHubBaseBranch = @"^[^\s]+$";
}