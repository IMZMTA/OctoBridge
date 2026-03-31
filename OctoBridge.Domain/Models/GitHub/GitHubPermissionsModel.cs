using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubPermissionsModel
{
    [JsonPropertyName("admin")]
    public bool Admin { get; set; }

    [JsonPropertyName("push")]
    public bool Push { get; set; }

    [JsonPropertyName("pull")]
    public bool Pull { get; set; }
}