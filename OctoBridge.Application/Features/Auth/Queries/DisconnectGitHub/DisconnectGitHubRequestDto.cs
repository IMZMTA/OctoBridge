using MediatR;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.Auth.Queries.DisconnectGitHub;

public class DisconnectGitHubRequestDto : IRequest<ApiResponse<DisconnectGitHubResponseDto>>
{

}
