using MediatR;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

public class ConnectGitHubByServerPATRequestDto : IRequest<ApiResponse<ConnectGitHubByServerPATResponseDto>>
{
    public string? ConnectorName { get; set; } = Messages.ConnectorName;

}
