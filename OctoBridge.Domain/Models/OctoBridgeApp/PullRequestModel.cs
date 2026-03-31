namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class PullRequestModel
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string Head { get; set; } = string.Empty;
    public string? HeadRepo { get; set; }
    public string Base { get; set; } = string.Empty;
    public string? Body { get; set; }
    public bool? Draft { get; set; }
    public bool? MaintainerCanModify { get; set; }
    public int? Issue { get; set; }
}