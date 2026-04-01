using System.Text.Json;
using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubApiResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess => Error == null;
    public string? LinkHeader { get; set; }
    public GitHubApiError? Error { get; set; }
}

public class GitHubApiError
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("documentation_url")]
    public string? DocumentationUrl { get; set; }
    [JsonPropertyName("errors")]
    public List<GitHubApiErrorDetail>? Errors { get; set; }

}

public class GitHubApiErrorDetail
{
    [JsonPropertyName("resource")]
    public string? Resource { get; set; }
    [JsonPropertyName("field")]
    public string? Field { get; set; }
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    [JsonPropertyName("value")]
    public JsonElement? Value { get; set; }
}

public class GitHubOAuthApiResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess => Error == null;
    public GitHubOAuthError? Error { get; set; }
}

public class GitHubOAuthError
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
    [JsonPropertyName("error_uri")]
    public string? ErrorUri { get; set; }

}