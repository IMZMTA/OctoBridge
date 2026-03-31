namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class IssueRequestModel
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public List<string>? Assignees { get; set; }
    public List<string>? Labels { get; set; }
    public string? Type { get; set; }
}