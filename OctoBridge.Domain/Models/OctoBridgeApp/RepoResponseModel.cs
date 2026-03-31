namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class RepoResponseModel
{
    public string RepositoryName { get; set; } = string.Empty; // repo short name
    public string FullName { get; set; } = string.Empty; // owner/repo
    public string OwnerLogin { get; set; } = string.Empty; // repo owner
    public bool IsPrivate { get; set; }
    public string HtmlUrl { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Language { get; set; }
    public int StargazersCount { get; set; }
    public int OpenIssuesCount { get; set; }
    public string DefaultBranch { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool HasIssues { get; set; }
    public PermissionsModel Permissions { get; set; } = new();
    public string? LabelsUrl { get; set; }
    public string? MilestonesUrl { get; set; }
    public string? AssigneesUrl { get; set; }
    public string? PullsUrl { get; set; }
}

public class PermissionsModel
{
    public bool Admin { get; set; }
    public bool Push { get; set; }
    public bool Pull { get; set; }
}