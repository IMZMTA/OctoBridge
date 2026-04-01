using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;
using OctoBridge.Infrastructure.Services.CookieService;

namespace OctoBridge.Application.Features.Auth.Queries.DisconnectGitHub;

public class DisconnectGitHubHandler : BaseHandler<DisconnectGitHubRequestDto, DisconnectGitHubResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly ICookieService cookieService;
    private readonly IUserContext userContext;

    public DisconnectGitHubHandler(IUserService _userService, IGitHubService _gitHubService, ICookieService _cookieService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        cookieService = _cookieService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<DisconnectGitHubResponseDto>> ProcessAsync(
        DisconnectGitHubRequestDto request,
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        if (userId == 0 || providerId == 0)
        {
            return new ApiResponse<DisconnectGitHubResponseDto>
            {
                Success = false,
                Messages = new List<string> { ErrorMessages.UserOrSessionExpired }
            };
        }

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<DisconnectGitHubResponseDto>
            {
                Success = false,
                Messages = new() { ErrorMessages.UserNotFound }
            };
        }

        await gitHubService.RevokeTokenAsync(user.AccessToken, cancellationToken);

        var removedUserId = await userService.RemoveUserAsync(userId, providerId, cancellationToken);

        cookieService.ClearJwtCookie();

        return new ApiResponse<DisconnectGitHubResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.DisconnectSuccess },
            Data = new DisconnectGitHubResponseDto()
            {
                UserId = removedUserId,
                UserName = userName,
            },
        };
    }
}