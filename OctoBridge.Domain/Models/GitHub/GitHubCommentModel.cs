using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubCommentModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonPropertyName("issue_url")]
    public string IssueUrl { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public GitHubUserModel? User { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("author_association")]
    public string? AuthorAssociation { get; set; }

    [JsonPropertyName("reactions")]
    public GitHubReactionsModel? Reactions { get; set; }
}