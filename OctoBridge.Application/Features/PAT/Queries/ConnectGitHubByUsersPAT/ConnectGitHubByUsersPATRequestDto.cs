using MediatR;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATRequestDto : IRequest<ApiResponse<ConnectGitHubByUsersPATResponseDto>>
{
    public string PersonalAccessToken { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"ConnectGitHubRequest: Connector={Messages.ConnectorName}, PAT=********";
    }
}
