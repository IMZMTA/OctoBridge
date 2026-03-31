using OctoBridge.Domain.Models.GitHub;

namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class RepositoriesResultModel<T>
{
    public IEnumerable<T> List { get; set; } = [];
    public string? LinkHeader { get; set; } = string.Empty;
}