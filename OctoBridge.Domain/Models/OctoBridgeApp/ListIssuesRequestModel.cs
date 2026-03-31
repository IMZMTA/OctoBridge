using OctoBridge.Domain.Enums;

namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class ListIssuesRequestModel
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public IssueState State { get; set; }
    public string Assignee { get; set; } = string.Empty;
    public List<string>? Labels { get; set; }
    public IssueSortType Sort { get; set; }
    public SortDirection Direction { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
    public DateTime? Since { get; set; }
}