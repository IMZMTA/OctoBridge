using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubLicenseModel
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
