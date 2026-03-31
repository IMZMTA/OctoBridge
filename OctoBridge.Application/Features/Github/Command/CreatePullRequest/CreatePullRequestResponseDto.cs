namespace OctoBridge.Application.Features.Github.Command.CreatePullRequest;

public class CreatePullRequestResponseDto
{
    public long GlobalId { get; set; }
    public int PullRequestNo { get; set; }
    public string Title { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public bool IsDraft { get; set; }
    public string Url { get; set; } = string.Empty;
    public string AuthorUsername { get; set; } = string.Empty;
    public string HeadBranch { get; set; } = string.Empty;
    public string BaseBranch { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
}
