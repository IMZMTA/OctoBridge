using OctoBridge.Domain.Constants;
using OctoBridge.Application.Common;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;

namespace OctoBridge.Application.Features.Github.Command.CreatePullRequest;

public class CreatePullRequestHandler : BaseHandler<CreatePullRequestRequestDto, CreatePullRequestResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly IUserContext userContext;

    public CreatePullRequestHandler(IUserService _userService, IGitHubService _gitHubService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<CreatePullRequestResponseDto>> ProcessAsync(CreatePullRequestRequestDto request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<CreatePullRequestResponseDto>
            {
                Success = false,
                Messages = new() { Messages.UserOrSessionExpired }
            };
        }

        var prRequest = new PullRequestModel
        {
            RepositoryOwner = request.RepositoryOwner,
            RepositoryName = request.RepositoryName,
            Title = request.Title,
            Head = request.Head,
            HeadRepo = request.HeadRepo,
            Base = request.Base,
            Body = request.Body,
            MaintainerCanModify = request.MaintainerCanModify,
            Draft = request.Draft,
            Issue = request.Issue,
        };

        var prResponse = await gitHubService.CreatePullRequestAsync(prRequest, user.AccessToken, cancellationToken);

        return new ApiResponse<CreatePullRequestResponseDto>()
        {
            Success = true,
            Messages = new List<string> { Messages.PRCreatedSuccessfully },
            Data = new CreatePullRequestResponseDto()
            {
                GlobalId = prResponse.Id,
                PullRequestNo = prResponse.Number,
                Url = prResponse.Url,
                Title = prResponse.Title,
                State = prResponse.State,
                IsDraft = prResponse.IsDraft,
                HeadBranch = prResponse.HeadBranch,
                BaseBranch = prResponse.BaseBranch,
                AuthorUsername = prResponse.AuthorUsername,
                CreatedAt = prResponse.CreatedAt,
            },
        };
    }
}
