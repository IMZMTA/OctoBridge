namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class CommitResponseModel
{
    public string Sha { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string HtmlUrl { get; set; } = string.Empty;
    public string? AuthorLogin { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}