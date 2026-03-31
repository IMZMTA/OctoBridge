using Microsoft.Extensions.Options;
using OctoBridge.Domain.Config;
using System.Security.Authentication;
using OctoBridge.Application.Common;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.GitHubService;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

public class ConnectGitHubByServerPATHandler : BaseHandler<ConnectGitHubByServerPATRequestDto, ConnectGitHubByServerPATResponseDto>
{
    private readonly IGitHubService gitHubService;
    private readonly AppSettings settings;

    public ConnectGitHubByServerPATHandler(IGitHubService _gitHubService, IOptions<AppSettings> _options)
    {
        gitHubService = _gitHubService;
        settings = _options.Value;

    }

    protected override async Task<ApiResponse<ConnectGitHubByServerPATResponseDto>> ProcessAsync(ConnectGitHubByServerPATRequestDto request, CancellationToken cancellationToken)
    {
        var pat = settings.Providers.GitHub.GitHubPAT;

        if (string.IsNullOrWhiteSpace(pat))
            throw new AuthenticationException("GitHub PAT not configured");

        var result = await gitHubService.GetUserProfileAsync(pat, cancellationToken);

        return new ApiResponse<ConnectGitHubByServerPATResponseDto>()
        {
            Success = true,
            Messages = new List<string> { Messages.PATLoginSuccess },
            Data = new ConnectGitHubByServerPATResponseDto()
            {
                ProviderId = result.ProviderId,
                Username = result.Username,
                AvatarUrl = result.AvatarUrl,
                ProfileUrl = result.ProfileUrl,
                ConnectionStatus = result.ConnectionStatus,
                ConnectedAt = result.ConnectedAt
            },
        };
    }
}
