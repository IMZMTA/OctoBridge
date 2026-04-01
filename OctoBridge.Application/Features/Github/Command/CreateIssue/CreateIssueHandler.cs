using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants.Messages;
using OctoBridge.Domain.Models.OctoBridgeApp;
using OctoBridge.Application.CQRS.Abstractions;
using OctoBridge.Infrastructure.Services.UserContext;
using OctoBridge.Infrastructure.Services.UserService;
using OctoBridge.Infrastructure.Services.GitHubService;

namespace OctoBridge.Application.Features.Github.Command.CreateIssue;

public class CreateIssueHandler : BaseHandler<CreateIssueRequestDto, CreateIssueResponseDto>
{
    private readonly IUserService userService;
    private readonly IGitHubService gitHubService;
    private readonly IUserContext userContext;

    public CreateIssueHandler(IUserService _userService, IGitHubService _gitHubService, IUserContext _userContext)
    {
        userService = _userService;
        gitHubService = _gitHubService;
        userContext = _userContext;
    }

    protected override async Task<ApiResponse<CreateIssueResponseDto>> ProcessAsync(CreateIssueRequestDto request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var providerId = userContext.ProviderId;
        var userName = userContext.UserName;

        var user = await userService.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<CreateIssueResponseDto>
            {
                Success = false,
                Messages = new() { ErrorMessages.UserOrSessionExpired }
            };
        }

        var issueRequest = new IssueRequestModel
        {
            RepositoryOwner = request.RepositoryOwner,
            RepositoryName = request.RepositoryName,
            Title = request.Title,
            Body = request.Body,
            Assignees = request.Assignees ?? [],
            Labels = request.Labels ?? [],
            Type = request.Type
        };

        var issueResponse = await gitHubService.CreateIssueRepositoryAsync(issueRequest, user.AccessToken, cancellationToken);

        return new ApiResponse<CreateIssueResponseDto>()
        {
            Success = true,
            Messages = new List<string> { SuccessMessages.IssueCreatedSuccessfully },
            Data = new CreateIssueResponseDto()
            {
                GlobalId = issueResponse.GlobalId,
                RepositoryId = issueResponse.IssueNumber,
                Url = issueResponse.Url,
                HtmlUrl = issueResponse.HtmlUrl,
                Title = issueResponse.Title,
                Body = issueResponse.Body,
                State = issueResponse.State,
                Assignee = issueResponse.Assignee,
                Labels = issueResponse.Labels,
                CreatedAt = issueResponse.CreatedAt,
                UpdatedAt = issueResponse.UpdatedAt,
            },
        };
    }
}
