using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;

namespace OctoBridge.Application.Features.Github.Queries.GetCommits;

public class GetCommitsHandler : BaseHandler<GetCommitsRequestDto, GetCommitsResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly IUserContext userContext;

    public GetCommitsHandler(IUserService _userService, IGitHubService _gitHubService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<GetCommitsResponseDto>> ProcessAsync(GetCommitsRequestDto request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<GetCommitsResponseDto>
            {
                Success = false,
                Messages = new() { ErrorMessages.UserOrSessionExpired }
            };
        }

        var commitRequest = new CommitRequestModel
        {
            RepositoryName = request.RepositoryName,
            RepositoryOwner = request.RepositoryOwner,
            Since = request.Since,
            Until = request.Until,
            Sha = request.Sha,
            Author = request.Author,
            PageNo = request.PageNo,
            PageSize = request.PageSize,
            Path = request.Path
        };

        var commitResponse = await gitHubService.GetCommitsAsync(commitRequest, user.AccessToken, cancellationToken);

        return new ApiResponse<GetCommitsResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.CommitsFetched },
            Data = new GetCommitsResponseDto()
            {
                UserId = userId,
                UserName = userName,
                Commits = commitResponse.ResponseList,
                Page = request.PageNo,
                PageSize = request.PageSize,
                HasNextPage = commitResponse.HasNextPage
            },
        };
    }
}
