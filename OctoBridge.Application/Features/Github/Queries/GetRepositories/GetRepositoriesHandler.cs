using OctoBridge.Domain.Enums;
using OctoBridge.Application.Common;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositories;

public class GetRepositoriesHandler : BaseHandler<GetRepositoriesRequestDto, GetRepositoriesResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly IUserContext userContext;

    public GetRepositoriesHandler(IUserService _userService, IGitHubService _gitHubService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<GetRepositoriesResponseDto>> ProcessAsync(GetRepositoriesRequestDto request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<GetRepositoriesResponseDto>
            {
                Success = false,
                Messages = new() { Messages.UserOrSessionExpired }
            };
        }

        var repoRequest = new RepoRequestModel
        {
            OwnerName = request.OwnerName,
            OwnerType = request.OwnerType,
            Sort = request.Sort,
            Direction = request.Direction,
            PageSize = request.PageSize,
            PageNo = request.Page,
            Since = request.Since
        };

        if (request.OwnerType == RepoOwnerType.Authenticated)
        {
            repoRequest.Visibility = request.Visibility;
            repoRequest.Affiliations = request.Affiliations;
            repoRequest.Type = null;
        }
        else
        {
            repoRequest.Type = request.Type;
            repoRequest.Visibility = null;
            repoRequest.Affiliations = null;
        }

        var repos = await gitHubService.GetRepositoriesAsync(repoRequest, user.AccessToken, cancellationToken);

        return new ApiResponse<GetRepositoriesResponseDto>()
        {
            Success = true,
            Messages = new List<string> { Messages.RepositoriesFetchedSuccess },
            Data = new GetRepositoriesResponseDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Repositories = repos.ResponseList,
                Page = request.Page,
                PageSize = request.PageSize,
                HasNextPage = repos.HasNextPage
            },
        };
    }
}
