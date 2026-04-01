using MediatR;
using OctoBridge.Domain.Common;

namespace OctoBridge.Application.Features.Auth.Queries.DisconnectGitHub;

public class DisconnectGitHubRequestDto : IRequest<ApiResponse<DisconnectGitHubResponseDto>>
{

}
