using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;

namespace OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

public class GetRepositoryIssuesHandler : BaseHandler<GetRepositoryIssuesListRequestDto, GetRepositoryIssuesListResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly IUserContext userContext;

    public GetRepositoryIssuesHandler(IUserService _userService, IGitHubService _gitHubService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<GetRepositoryIssuesListResponseDto>> ProcessAsync(GetRepositoryIssuesListRequestDto request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<GetRepositoryIssuesListResponseDto>
            {
                Success = false,
                Messages = new() { ErrorMessages.UserOrSessionExpired }
            };
        }

        var issueRequestModel = new ListIssuesRequestModel
        {
            RepositoryOwner = request.RepositoryOwner,
            RepositoryName = request.RepositoryName,
            State = request.State,
            Assignee = request.Assignee ?? string.Empty,
            Labels = request.Labels,
            Sort = request.Sort,
            Direction = request.Direction,
            PageSize = request.PageSize,
            PageNo = request.PageNo,
            Since = request.Since
        };

        var repoIssues = await gitHubService.GetRepositoryIssuesAsync(issueRequestModel, user.AccessToken, cancellationToken);

        return new ApiResponse<GetRepositoryIssuesListResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.RepositoryIssuesFetched },
            Data = new GetRepositoryIssuesListResponseDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Issues = repoIssues.ResponseList,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                HasNextPage = repoIssues.HasNextPage
            },
        };
    }
}
