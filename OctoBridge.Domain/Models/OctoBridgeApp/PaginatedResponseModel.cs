namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class PaginatedResponseModel<T>
{
    public List<T> ResponseList { get; set; } = new();
    public bool HasNextPage { get; set; }
}