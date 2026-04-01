using OctoBridge.Domain.Models;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.OAuthService;
using OctoBridge.Infrastructure.Services.TokenService;
using OctoBridge.Infrastructure.Services.CookieService;

namespace OctoBridge.Application.Features.Auth.Queries.HandleOAuthCallback;

public class HandleOAuthCallbackHandler : BaseHandler<HandleOAuthCallbackRequestDto, HandleOAuthCallbackResponseDto>
{
    private readonly IEnumerable<IOAuthService> authServices;
    private readonly ITokenService tokenService;
    private readonly ICookieService cookieService;

    public HandleOAuthCallbackHandler(IEnumerable<IOAuthService> _authServices, ITokenService _tokenService, ICookieService _cookieService)
    {
        authServices = _authServices;
        tokenService = _tokenService;
        cookieService = _cookieService;
    }

    protected override async Task<ApiResponse<HandleOAuthCallbackResponseDto>> ProcessAsync(
        HandleOAuthCallbackRequestDto request,
        CancellationToken cancellationToken)
    {
        var service = authServices.FirstOrDefault(s => s.Provider == request.Provider);

        if (service == null)
        {
            return new ApiResponse<HandleOAuthCallbackResponseDto>
            {
                Success = false,
                Messages = new List<string> { string.Format(ValidationMessages.ProviderNotSupported, request.Provider) }
            };
        }

        var tokenModel = await service.GetAccessTokenAsync(request.Code, cancellationToken);

        var user = new UserModel
        {
            UserId = tokenModel.UserId ?? 0,
            UserName = tokenModel.UserName ?? string.Empty,
            Email = tokenModel.Email ?? string.Empty,
            ProviderId = tokenModel.ProviderId ?? 0
        };

        var jwt = tokenService.GenerateAccessToken(user);

        cookieService.SetJwtCookie(jwt);

        return new ApiResponse<HandleOAuthCallbackResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.OAuthLoginSuccess },
            Data = new HandleOAuthCallbackResponseDto()
            {
                UserId = tokenModel.UserId,
                ProviderId = tokenModel.ProviderId,
                UserName = tokenModel.UserName,
                Email = tokenModel.Email,
                TokenType = tokenModel.TokenType,
                IssuedAt = tokenModel.IssuedAt,
                ExpiresAt = tokenModel.ExpiresAt,
            },
        };
    }
}