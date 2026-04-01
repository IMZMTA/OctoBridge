using MediatR;
using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Common;

namespace OctoBridge.Application.Features.Auth.Queries.LoginProviderUrl;

public class LoginProviderUrlRequestDto : IRequest<ApiResponse<LoginProviderUrlResponseDto>>
{
    public AuthProvider Provider { get; set; } = AuthProvider.GitHub;
}
