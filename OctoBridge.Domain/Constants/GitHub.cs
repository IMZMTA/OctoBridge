namespace OctoBridge.Domain.Constants;

public static class GitHubEndpoints
{
    public const string UserProfile = "user";
    public const string UserRepos = "user/repos";
    public const string UserOrgs = "user/orgs";

    public const string UserReposTemplate = "users/{0}/repos";
    public const string OrgReposTemplate = "orgs/{0}/repos";
    public const string CreateIssuesTemplate = "repos/{0}/{1}/issues";
    public const string CreatePullRequestTemplate = "repos/{0}/{1}/pulls";
    public const string GetCommitsTemplate = "repos/{0}/{1}/commits";
}


public static class GitHubConstants
{
    public const string GitHubApplicationJson = "application/vnd.github+json";
    public const int MaxPageSize = 100;
    public const int DefaultPageSize = 30;
    public const int DefaultPage = 1;
    public const string DefaultBranch = "main";
    public const string Link = "Link";
}