using MediatR;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

public class ConnectGitHubByServerPATRequestDto : IRequest<ApiResponse<ConnectGitHubByServerPATResponseDto>>
{
    public string? ConnectorName { get; set; } = AppConstants.ConnectorName;

}
