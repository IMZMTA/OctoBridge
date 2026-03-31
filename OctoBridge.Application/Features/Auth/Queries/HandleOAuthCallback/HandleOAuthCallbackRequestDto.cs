using MediatR;
using OctoBridge.Domain.Enums;
using OctoBridge.Application.Common;

namespace OctoBridge.Application.Features.Auth.Queries.HandleOAuthCallback;

public class HandleOAuthCallbackRequestDto : IRequest<ApiResponse<HandleOAuthCallbackResponseDto>>
{
    public AuthProvider Provider { get; set; } = AuthProvider.GitHub;
    public string Code { get; set; } = string.Empty;
}
