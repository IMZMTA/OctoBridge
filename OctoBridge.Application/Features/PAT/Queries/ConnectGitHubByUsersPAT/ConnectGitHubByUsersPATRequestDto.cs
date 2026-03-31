using MediatR;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATRequestDto : IRequest<ApiResponse<ConnectGitHubByUsersPATResponseDto>>
{
    public string PersonalAccessToken { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"ConnectGitHubRequest: Connector={AppConstants.ConnectorName}, PAT=********";
    }
}
