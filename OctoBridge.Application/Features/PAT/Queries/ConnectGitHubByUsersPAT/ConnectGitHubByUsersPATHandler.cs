using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.GitHubService;

namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATHandler : BaseHandler<ConnectGitHubByUsersPATRequestDto, ConnectGitHubByUsersPATResponseDto>
{
    private readonly IGitHubService gitHubService;

    public ConnectGitHubByUsersPATHandler(IGitHubService _gitHubService)
    {
        gitHubService = _gitHubService;
    }

    protected override async Task<ApiResponse<ConnectGitHubByUsersPATResponseDto>> ProcessAsync(ConnectGitHubByUsersPATRequestDto request, CancellationToken cancellationToken)
    {

        var result = await gitHubService.GetUserProfileAsync(request.PersonalAccessToken, cancellationToken);

        return new ApiResponse<ConnectGitHubByUsersPATResponseDto>()
        {
            Success = true,
            Messages = new List<string> { Messages.PATLoginSuccess },
            Data = new ConnectGitHubByUsersPATResponseDto()
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
