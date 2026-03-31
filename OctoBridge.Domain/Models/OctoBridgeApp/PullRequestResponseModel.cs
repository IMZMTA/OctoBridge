namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class PullRequestResponseModel
{
    public long Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public bool IsDraft { get; set; }
    public string Url { get; set; } = string.Empty;
    public string AuthorUsername { get; set; } = string.Empty;
    public string? AuthorAvatarUrl { get; set; }

    public string HeadBranch { get; set; } = string.Empty;
    public string BaseBranch { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}