using MediatR;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.Auth.Queries.Logout;

public class LogoutRequestDto : IRequest<ApiResponse<LogoutResponseDto>>
{

}
