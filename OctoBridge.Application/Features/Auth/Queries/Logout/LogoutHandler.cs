using OctoBridge.Application.Common;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Domain.Constants;
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
            Messages = new List<string> { Messages.LogoutSuccessfully },
            Data = null,
        };
    }
}
