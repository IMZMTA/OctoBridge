using MediatR;
using OctoBridge.Domain.Common;

namespace OctoBridge.Application.Features.Auth.Queries.Logout;

public class LogoutRequestDto : IRequest<ApiResponse<LogoutResponseDto>>
{

}
