using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubIssueModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public GitHubUserModel? User { get; set; }

    [JsonPropertyName("assignee")]
    public GitHubUserModel? Assignee { get; set; }

    [JsonPropertyName("assignees")]
    public List<GitHubUserModel>? Assignees { get; set; }

    [JsonPropertyName("labels")]
    public List<GitHubLabelModel>? Labels { get; set; }

    [JsonPropertyName("milestone")]
    public GitHubMilestoneModel? Milestone { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("closed_at")]
    public DateTime? ClosedAt { get; set; }

    [JsonPropertyName("locked")]
    public bool Locked { get; set; }

    [JsonPropertyName("active_lock_reason")]
    public string? ActiveLockReason { get; set; }

    [JsonPropertyName("pull_request")]
    public GitHubPullRequestModel? PullRequest { get; set; }

    [JsonPropertyName("author_association")]
    public string? AuthorAssociation { get; set; }

    [JsonPropertyName("state_reason")]
    public string? StateReason { get; set; }

    [JsonPropertyName("comments")]
    public int? Comments { get; set; }
}
