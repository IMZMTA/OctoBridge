using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubUserModel
{
    [JsonPropertyName("login")]
    public string Login { get; init; } = default!;

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; init; } = default!;

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; init; } = default!;

    [JsonPropertyName("gravatar_id")]
    public string? GravatarId { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; } = default!;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; init; } = default!;

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("company")]
    public string? Company { get; init; }

    [JsonPropertyName("blog")]
    public string? Blog { get; init; }

    [JsonPropertyName("location")]
    public string? Location { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("bio")]
    public string? Bio { get; init; }

    [JsonPropertyName("twitter_username")]
    public string? TwitterUsername { get; init; }

    [JsonPropertyName("public_repos")]
    public int PublicRepos { get; init; }

    [JsonPropertyName("public_gists")]
    public int PublicGists { get; init; }

    [JsonPropertyName("followers")]
    public int Followers { get; init; }

    [JsonPropertyName("following")]
    public int Following { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}