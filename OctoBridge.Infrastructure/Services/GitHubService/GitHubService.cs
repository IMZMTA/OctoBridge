using System.Web;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Constants;
using System.Text.RegularExpressions;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Infrastructure.Clients.GitHubClient;

namespace OctoBridge.Infrastructure.Services.GitHubService;

public class GitHubService : IGitHubService
{
    private readonly IGitHubClient client;

    public GitHubService(IGitHubClient _client)
    {
        client = _client;
    }

    public async Task<UserResponseModel> GetUserProfileAsync(string accessToken, CancellationToken cancellationToken)
    {
        var rawData = await client.GetUserAsync(accessToken, cancellationToken);

        return new UserResponseModel
        {
            ProviderId = rawData.Id,
            Username = rawData.Login,
            AvatarUrl = rawData.AvatarUrl,
            ProfileUrl = rawData.HtmlUrl,
            ConnectionStatus = GitHubConnectionStatus.Connected.ToString(),
            ConnectedAt = DateTime.UtcNow
        };
    }

    public async Task<bool> RevokeTokenAsync(string accessToken, CancellationToken cancellationToken)
    {
        var IsTokenRevoked = await client.RevokeTokenAsync(accessToken, cancellationToken);
        return IsTokenRevoked;
    }

    public async Task<PaginatedResponseModel<RepoResponseModel>> GetRepositoriesAsync(RepoRequestModel request, string accessToken, CancellationToken cancellationToken)
    {
        var url = BuildUrl(request);

        var rawResponse = await client.GetRepositoriesAsync(url, accessToken, cancellationToken);

        if (rawResponse == null || !rawResponse.List.Any())
        {
            return new PaginatedResponseModel<RepoResponseModel>();
        }

        var repoResponses = rawResponse.List.Select(repo => new RepoResponseModel
        {
            RepositoryName = repo.Name,
            FullName = repo.FullName,
            OwnerLogin = repo.Owner.Login,
            IsPrivate = repo.IsPrivate,
            HtmlUrl = repo.HtmlUrl,
            Description = repo.Description,
            Language = repo.Language,
            StargazersCount = repo.StargazersCount,
            OpenIssuesCount = repo.OpenIssuesCount,
            DefaultBranch = repo.DefaultBranch ?? GitHubConstants.DefaultBranch,
            CreatedAt = repo.CreatedAt,
            UpdatedAt = repo.UpdatedAt,
            HasIssues = repo.HasIssues,
            Permissions = new PermissionsModel
            {
                Admin = repo.Permissions?.Admin ?? false,
                Push = repo.Permissions?.Push ?? false,
                Pull = repo.Permissions?.Pull ?? false
            },
            LabelsUrl = repo.LabelsUrl,
            MilestonesUrl = repo.MilestonesUrl,
            AssigneesUrl = repo.AssigneesUrl,
            PullsUrl = repo.PullsUrl
        }).ToList();

        return new PaginatedResponseModel<RepoResponseModel>
        {
            ResponseList = repoResponses,
            HasNextPage = HasNextPage(rawResponse.LinkHeader)
        };
    }

    public async Task<IssueResponseModel> CreateIssueRepositoryAsync(IssueRequestModel request, string accessToken, CancellationToken cancellationToken)
    {
        var url = string.Format(GitHubEndpoints.CreateIssuesTemplate, request.RepositoryOwner, request.RepositoryName);

        var rawIssue = await client.CreateIssueAsync(request, url, accessToken, cancellationToken);

        return new IssueResponseModel
        {
            GlobalId = rawIssue.Id,
            IssueNumber = rawIssue.Number,
            Url = rawIssue.Url,
            HtmlUrl = rawIssue.HtmlUrl,
            Title = rawIssue.Title,
            Body = rawIssue.Body,
            State = rawIssue.State,
            Assignee = rawIssue.Assignee?.Login,
            Assignees = rawIssue.Assignees?.Select(a => a.Login).ToList() ?? new List<string>(),
            Labels = rawIssue.Labels?.Select(l => l.Name).ToList() ?? new List<string>(),
            Milestone = rawIssue.Milestone?.Title ?? string.Empty,
            Locked = rawIssue.Locked,
            ActiveLockReason = rawIssue.ActiveLockReason,
            CreatedAt = rawIssue.CreatedAt,
            UpdatedAt = rawIssue.UpdatedAt,
            Comments = rawIssue.Comments ?? AppConstants.Zero
        };
    }

    public async Task<PaginatedResponseModel<IssueResponseModel>> GetRepositoryIssuesAsync(ListIssuesRequestModel request, string accessToken, CancellationToken cancellationToken)
    {
        var url = BuildIssuesUrl(request);
        var rawIssues = await client.GetRepositoryIssuesAsync(url, accessToken, cancellationToken);

        if (rawIssues == null || !rawIssues.List.Any())
        {
            return new PaginatedResponseModel<IssueResponseModel>();
        }

        var issueResponses = rawIssues.List.Select(issue => new IssueResponseModel
        {
            GlobalId = issue.Id,
            IssueNumber = issue.Number,
            Url = issue.Url,
            HtmlUrl = issue.HtmlUrl,
            Title = issue.Title,
            Body = issue.Body,
            State = issue.State,
            Assignee = issue.Assignee?.Login,
            Labels = issue.Labels?.Select(l => l.Name).ToList() ?? new List<string>(),
            Milestone = issue.Milestone?.Title ?? string.Empty,
            Locked = issue.Locked,
            ActiveLockReason = issue.ActiveLockReason,
            CreatedAt = issue.CreatedAt,
            UpdatedAt = issue.UpdatedAt,
            Comments = issue.Comments ?? AppConstants.Zero
        }).ToList();

        return new PaginatedResponseModel<IssueResponseModel>
        {
            ResponseList = issueResponses,
            HasNextPage = HasNextPage(rawIssues.LinkHeader)
        };
    }

    public async Task<PaginatedResponseModel<CommitResponseModel>> GetCommitsAsync(CommitRequestModel request, string accessToken, CancellationToken cancellationToken)
    {
        var url = BuildCommitsUrl(request);
        var rawCommits = await client.GetCommitsAsync(url, accessToken, cancellationToken);

        if (rawCommits == null || !rawCommits.List.Any())
        {
            return new PaginatedResponseModel<CommitResponseModel>();
        }

        var commitResponses = rawCommits.List.Select(commit => new CommitResponseModel
        {
            Sha = commit.Sha,
            Message = commit.Commit?.Message ?? string.Empty,
            AuthorName = commit.Commit?.Author?.Name ?? string.Empty,
            AuthorLogin = commit.Commit?.Author?.Email ?? string.Empty,
            Date = commit.Commit?.Author?.Date ?? default,
            HtmlUrl = commit.HtmlUrl
        }).ToList();

        return new PaginatedResponseModel<CommitResponseModel>
        {
            ResponseList = commitResponses,
            HasNextPage = HasNextPage(rawCommits.LinkHeader)
        };
    }

    public async Task<PullRequestResponseModel> CreatePullRequestAsync(PullRequestModel request, string accessToken, CancellationToken cancellationToken)
    {
        var url = string.Format(GitHubEndpoints.CreatePullRequestTemplate, request.RepositoryOwner, request.RepositoryName);

        var rawPR = await client.CreatePullRequestAsync(request, url, accessToken, cancellationToken);

        return new PullRequestResponseModel
        {
            Id = rawPR.Id,
            Number = rawPR.Number,
            Title = rawPR.Title,
            State = rawPR.State,
            IsDraft = rawPR.Draft,
            Url = rawPR.HtmlUrl,
            AuthorUsername = rawPR.User?.Login ?? string.Empty,
            AuthorAvatarUrl = rawPR.User?.AvatarUrl,
            HeadBranch = rawPR.Head?.Label ?? string.Empty,
            BaseBranch = rawPR.Base?.Label ?? string.Empty,
            CreatedAt = rawPR.CreatedAt 
        };
    }

    private static string BuildUrl(RepoRequestModel request)
    {

        string basePath = request.OwnerType switch
        {
            RepoOwnerType.Authenticated => GitHubEndpoints.UserRepos,
            RepoOwnerType.User => string.Format(GitHubEndpoints.UserReposTemplate, request.OwnerName),
            RepoOwnerType.Organization => string.Format(GitHubEndpoints.OrgReposTemplate, request.OwnerName),
            _ => throw new ArgumentException("Invalid OwnerType")
        };

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["sort"] = request.Sort.ToString().ToLower();
        query["direction"] = request.Direction.ToString().ToLower();
        query["per_page"] = request.PageSize.ToString();
        query["page"] = request.PageNo.ToString();

        if (request.Since.HasValue)
        {
            query["since"] = request.Since.Value.ToString("O");
        }

        if (request.OwnerType == RepoOwnerType.Authenticated)
        {
            bool hasVisibility = request.Visibility.HasValue && request.Visibility != RepoVisibility.All;
            bool hasAffiliation = request.Affiliations?.Any() == true;
            if (hasVisibility)
            {
                query["visibility"] = request.Visibility!.Value.ToString().ToLower();
            }

            if (hasAffiliation)
            {
                var affs = request.Affiliations!.Select(a =>
                    a.ToString().ToLower() == "organizationmember" ? "organization_member" : a.ToString().ToLower());
                query["affiliation"] = string.Join(",", affs);
            }

            if (!hasVisibility && !hasAffiliation && request.Type.HasValue)
            {
                query["type"] = request.Type.Value.ToString().ToLower();
            }
        }
        else if (request.Type.HasValue)
        {
            query["type"] = request.Type.Value.ToString().ToLower();
        }

        return $"{basePath}?{query}";
    }

    private static int GetLastPage(string? linkHeader)
    {
        if (string.IsNullOrEmpty(linkHeader)) return AppConstants.Zero;

        var match = Regex.Match(linkHeader, @"page=(\d+)>; rel=""last""");
        if (match.Success && int.TryParse(match.Groups[1].Value, out var lastPage))
        {
            return lastPage;
        }

        return AppConstants.Zero;
    }

    private static bool HasNextPage(string? linkHeader)
    {
        if (string.IsNullOrEmpty(linkHeader))
            return false;

        return linkHeader.Contains("rel=\"next\"");
    }

    private static string BuildIssuesUrl(ListIssuesRequestModel request)
    {
        var baseUrl = string.Format(GitHubEndpoints.CreateIssuesTemplate, request.RepositoryOwner, request.RepositoryName);

        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);

        query["state"] = request.State.ToString().ToLower();
        query["sort"] = request.Sort.ToString().ToLower();
        query["direction"] = request.Direction.ToString().ToLower();
        query["per_page"] = request.PageSize.ToString();
        query["page"] = request.PageNo.ToString();

        if (request.Labels != null && request.Labels.Count != AppConstants.Zero)
        {
            query["labels"] = string.Join(",", request.Labels);
        }

        if (!string.IsNullOrEmpty(request.Assignee))
            query["assignee"] = request.Assignee;

        if (request.Since.HasValue)
            query["since"] = request.Since.Value.ToString("O");

        return $"{baseUrl}?{query}";
    }

    private static string BuildCommitsUrl(CommitRequestModel request)
    {
        var baseUrl = string.Format(GitHubEndpoints.GetCommitsTemplate, request.RepositoryOwner, request.RepositoryName);

        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrEmpty(request.Sha))
            query["sha"] = request.Sha;

        if (!string.IsNullOrEmpty(request.Path))
            query["path"] = request.Path;

        if (!string.IsNullOrEmpty(request.Author))
            query["author"] = request.Author;

        if (request.Since.HasValue)
            query["since"] = request.Since.Value.ToString("O");

        if (request.Until.HasValue)
            query["until"] = request.Until.Value.ToString("O");

        query["per_page"] = request.PageSize.ToString();
        query["page"] = request.PageNo.ToString();

        return $"{baseUrl}?{query}";
    }

}
