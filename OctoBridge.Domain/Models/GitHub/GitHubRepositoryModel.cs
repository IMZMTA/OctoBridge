using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubRepositoryModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("full_name")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("owner")]
    public GitHubOwnerModel Owner { get; set; } = default!;

    [JsonPropertyName("private")]
    public bool IsPrivate { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("fork")]
    public bool IsFork { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("pushed_at")]
    public DateTime PushedAt { get; set; }

    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }

    [JsonPropertyName("watchers_count")]
    public int WatchersCount { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("forks_count")]
    public int ForksCount { get; set; }

    [JsonPropertyName("open_issues_count")]
    public int OpenIssuesCount { get; set; }

    [JsonPropertyName("default_branch")]
    public string DefaultBranch { get; set; } = string.Empty;

    [JsonPropertyName("license")]
    public GitHubLicenseModel? License { get; set; }

    [JsonPropertyName("permissions")]
    public GitHubPermissionsModel Permissions { get; set; } = new();

    // URLs for creating or fetching issues, labels, milestones, pulls, etc.
    [JsonPropertyName("issues_url")]
    public string IssuesUrl { get; set; } = string.Empty;

    [JsonPropertyName("labels_url")]
    public string LabelsUrl { get; set; } = string.Empty;

    [JsonPropertyName("milestones_url")]
    public string MilestonesUrl { get; set; } = string.Empty;

    [JsonPropertyName("assignees_url")]
    public string AssigneesUrl { get; set; } = string.Empty;

    [JsonPropertyName("pulls_url")]
    public string PullsUrl { get; set; } = string.Empty;

    [JsonPropertyName("topics")]
    public List<string> Topics { get; set; } = new();

    [JsonPropertyName("has_issues")]
    public bool HasIssues { get; set; }

    [JsonPropertyName("has_projects")]
    public bool HasProjects { get; set; }

    [JsonPropertyName("has_wiki")]
    public bool HasWiki { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }
}
