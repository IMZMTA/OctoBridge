using OctoBridge.Domain.Enums;

namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class RepoRequestModel
{
    public string? OwnerName { get; set; }
    public RepoOwnerType OwnerType { get; set; }
    public RepoVisibility? Visibility { get; set; }
    public List<RepoAffiliation>? Affiliations { get; set; }
    public RepoType? Type { get; set; }
    public RepoSort Sort { get; set; }
    public SortDirection Direction { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
    public DateTime? Since { get; set; }
}