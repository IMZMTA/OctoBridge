using Microsoft.AspNetCore.Mvc;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Features.Github.Queries.GetCommits;
using OctoBridge.Application.Features.Github.Command.CreateIssue;
using OctoBridge.Application.Features.Github.Queries.GetRepositories;
using OctoBridge.Application.Features.Github.Command.CreatePullRequest;
using OctoBridge.Application.Features.Github.Queries.GetRepositoryIssuesList;

namespace OctoBridge.Api.Controllers.GitHubController
{
    [ApiController]
    [Route(ApiRoutes.Github.Root)]
    public class GitHubController : ApiController
    {

        /// <summary>
        /// Retrieves the authenticated user's repositories with optional filters and pagination.
        /// </summary>
        /// <param name="request">Request parameters for filtering and pagination</param>
        /// <returns>Paginated list of the user's repositories</returns>
        [HttpGet("repositories")]
        [ProducesResponseType(typeof(ApiResponse<GetRepositoriesResponseDto>), 200)]
        public async Task<IActionResult> GetRepositories([FromQuery] GetRepositoriesRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves issues from a specified repository with optional filters and pagination (Repository Owner and Repository Name must be provided).
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of issues</returns>
        [HttpGet("list-issues")]
        [ProducesResponseType(typeof(ApiResponse<GetRepositoryIssuesListResponseDto>), 200)]
        public async Task<IActionResult> GetIssues([FromQuery] GetRepositoryIssuesListRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves commits from a specified repository with optional filters and pagination (Repository Owner and Repository Name must be provided).
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of commits</returns>
        [HttpGet("get-commits")]
        [ProducesResponseType(typeof(ApiResponse<GetCommitsResponseDto>), 200)]
        public async Task<IActionResult> GetCommits([FromQuery] GetCommitsRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new issue in a specified repository.
        /// </summary>
        /// <param name="request">Details of the issue to create</param>
        /// <returns>Created issue details</returns>
        [HttpPost("create-issues")]
        [ProducesResponseType(typeof(ApiResponse<CreateIssueResponseDto>), 200)]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Create a pull request with either a Title or Issue Number. Use user:branch and specify HeadRepo for cross-repo PRs, or just the branch name for standard ones.
        /// </summary>
        /// <param name="request">Details of the pull request to create</param>
        /// <returns>Created pull request details</returns>
        [HttpPost("create-pull-requests")]
        [ProducesResponseType(typeof(ApiResponse<CreatePullRequestResponseDto>), 200)]
        public async Task<IActionResult> CreatePullRequest([FromBody] CreatePullRequestRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}
