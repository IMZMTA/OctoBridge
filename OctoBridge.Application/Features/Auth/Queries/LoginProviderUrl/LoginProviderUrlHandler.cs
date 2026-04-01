using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.OAuthService;

namespace OctoBridge.Application.Features.Auth.Queries.LoginProviderUrl;

public class LoginProviderUrlHandler : BaseHandler<LoginProviderUrlRequestDto, LoginProviderUrlResponseDto>
{
    private readonly IEnumerable<IOAuthService> authServices;

    public LoginProviderUrlHandler(IEnumerable<IOAuthService> _authServices)
    {
        authServices = _authServices;
    }

    protected override async Task<ApiResponse<LoginProviderUrlResponseDto>> ProcessAsync(
        LoginProviderUrlRequestDto request,
        CancellationToken cancellationToken)
    {
        var service = authServices.FirstOrDefault(s => s.Provider == request.Provider);

        if (service == null)
        {
            return new ApiResponse<LoginProviderUrlResponseDto>
            {
                Success = false,
                Messages = new List<string> { string.Format(ValidationMessages.ProviderNotSupported, request.Provider)  }
            };
        }

        var redirectUrl = service.GetAuthorizationUrl();

        return new ApiResponse<LoginProviderUrlResponseDto>()
        {
            Success = true,
            Messages = new List<string> { string.Format(SuccessMessages.RedirectUrlGenerated, request.Provider) },
            Data = new LoginProviderUrlResponseDto()
            {
                RedirectUrl = redirectUrl,
                Provider = request.Provider.ToString()
            },
        };
    }
}