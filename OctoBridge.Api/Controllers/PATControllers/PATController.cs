using Microsoft.AspNetCore.Mvc;
using OctoBridge.Domain.Common;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;
using OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByServerPAT;

namespace OctoBridge.Api.Controllers.PATControllers
{
    [ApiController]
    [Route(ApiRoutes.PAT.Root)]
    public class PATController : ApiController
    {

        /// <summary>
        /// Connects to GitHub using a server-stored PAT(Personal Access Token), validating it and fetching the user's profile data.
        /// </summary>
        /// <param name="request">Server PAT Details</param>
        /// <returns>ApiResponse with User Profile if PAT is valid</returns>
        [HttpGet("via-server")]
        [ProducesResponseType(typeof(ApiResponse<ConnectGitHubByServerPATResponseDto>), 200)]
        public async Task<IActionResult> ConnectServerPAT([FromQuery] ConnectGitHubByServerPATRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Connects to GitHub using a user-provided PAT(Personal Access Token) to validate and fetch profile data (not recommended for production).
        /// </summary>
        /// <body name="request">User Provided PAT Details</body>
        /// <returns>ApiResponse with User Profile if PAT is valid</returns>
        [HttpPost("via-users")]
        [ProducesResponseType(typeof(ApiResponse<ConnectGitHubByUsersPATResponseDto>), 200)]
        public async Task<IActionResult> ConnectPAT([FromBody] ConnectGitHubByUsersPATRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

    }
}
