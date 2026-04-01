using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.CookieService;

namespace OctoBridge.Application.Features.Auth.Queries.Logout;

public class LogoutHandler : BaseHandler<LogoutRequestDto, LogoutResponseDto>
{
    private readonly ICookieService cookieService;

    public LogoutHandler(ICookieService _cookieService)
    {
        cookieService = _cookieService;
    }

    protected override async Task<ApiResponse<LogoutResponseDto>> ProcessAsync(LogoutRequestDto request, CancellationToken cancellationToken)
    {
        cookieService.ClearJwtCookie();
        return new ApiResponse<LogoutResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.LogoutSuccess },
            Data = null,
        };
    }
}
