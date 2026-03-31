using OctoBridge.Domain.Models.OctoBridgeApp;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositories;

public class GetRepositoriesResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public List<RepoResponseModel> Repositories { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
}
