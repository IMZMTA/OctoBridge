using OctoBridge.Domain.Enums;

namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class CommitRequestModel
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string? Sha { get; set; }
    public string? Path { get; set; }
    public string? Author { get; set; }
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
}