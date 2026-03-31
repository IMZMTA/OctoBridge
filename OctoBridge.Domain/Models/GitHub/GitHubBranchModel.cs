using System.Text.Json.Serialization;

namespace OctoBridge.Domain.Models.GitHub;

public class GitHubBranchModel
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    [JsonPropertyName("sha")]
    public string? Sha { get; set; }

    [JsonPropertyName("repo")]
    public GitHubRepositoryModel? Repository { get; set; }
}
