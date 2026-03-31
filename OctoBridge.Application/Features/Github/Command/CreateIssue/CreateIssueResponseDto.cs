namespace OctoBridge.Application.Features.Github.Command.CreateIssue;

public class CreateIssueResponseDto
{
    public long GlobalId { get; set; }
    public int RepositoryId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string HtmlUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public string State { get; set; } = string.Empty;
    public string? Assignee { get; set; }
    public List<string>? Assignees { get; set; }
    public List<string>? Labels { get; set; }
    public string? Milestone { get; set; }
    public bool Locked { get; set; }
    public string? ActiveLockReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
