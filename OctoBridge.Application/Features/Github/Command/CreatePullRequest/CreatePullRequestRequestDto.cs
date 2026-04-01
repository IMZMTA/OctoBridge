using MediatR;
using OctoBridge.Domain.Common;

namespace OctoBridge.Application.Features.Github.Command.CreatePullRequest;

public class CreatePullRequestRequestDto : IRequest<ApiResponse<CreatePullRequestResponseDto>>
{
    public string RepositoryOwner { get; set; } = string.Empty;
    public string RepositoryName { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string Head { get; set; } = default!;
    public string? HeadRepo { get; set; }
    public string Base { get; set; } = default!;
    public string? Body { get; set; }
    public bool? MaintainerCanModify { get; set; }
    public bool? Draft { get; set; }
    public int? Issue { get; set; }
}
