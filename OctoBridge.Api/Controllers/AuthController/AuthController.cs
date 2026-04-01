using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using OctoBridge.Domain.Constants;
using OctoBridge.Application.Features.Auth.Queries.Logout;
using OctoBridge.Application.Features.Auth.Queries.DisconnectGitHub;
using OctoBridge.Application.Features.Auth.Queries.LoginProviderUrl;
using OctoBridge.Application.Features.Auth.Queries.HandleOAuthCallback;

namespace OctoBridge.Api.Controllers.AuthController
{
    [ApiController]
    [Route(ApiRoutes.Auth.Root)]
    public class AuthController : ApiController
    {

        /// <summary>
        /// Initiates the OAuth login process by generating a provider-specific authorization URL and redirecting the user to it.
        /// </summary>
        /// <param name="request">User Login details</param>
        /// <returns>Redirect to the OAuth provider on browser or get redirect URL</returns>
        [HttpGet("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginProviderUrlResponseDto>), 200)]
        public async Task<IActionResult> Login([FromQuery] LoginProviderUrlRequestDto request)
        {
            var result = await Mediator.Send(request);
            if (!result.Success || result.Data == null) return BadRequest(result);

            if (Request.Headers.Accept.ToString().Contains("application/json"))
            {
                return Ok(result);
            }

            return Redirect(result.Data.RedirectUrl);
        }

        /// <summary>
        /// Handles OAuth callback by exchanging the authorization code for a token, retrieving user data, and attaching a JWT cookie via HTTP Only Cookie.
        /// </summary>
        /// <param name="provider">The OAuth provider (e.g., GitHub)</param>
        /// <param name="code">The authorization code received from the OAuth provider</param>
        /// <returns>Return the user login info</returns>
        [HttpGet("{provider}/callback")]
        [ProducesResponseType(typeof(ApiResponse<HandleOAuthCallbackResponseDto>), 200)]
        public async Task<IActionResult> Callback(AuthProvider provider, [FromQuery] string code)
        {
            var result = await Mediator.Send(new HandleOAuthCallbackRequestDto
            {
                Code = code,
                Provider = provider
            });
            return Ok(result);
        }

        /// <summary>
        /// Logs out the current user by clearing their authentication cookie and ending their session.
        /// </summary>
        /// <returns>Return the logout info</returns>
        [HttpGet("logout")]
        [ProducesResponseType(typeof(ApiResponse<LogoutResponseDto>), 200)]
        public async Task<IActionResult> Logout()
        {
            var result = await Mediator.Send(new LogoutRequestDto());
            return Ok(result);
        }

        /// <summary>
        /// Unlinks the user's GitHub account by removing stored tokens and associations.
        /// </summary>
        /// <returns>ApiResponse indicating disconnection success</returns>
        [HttpDelete("disconnect")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> Disconnect()
        {
            var result = await Mediator.Send(new DisconnectGitHubRequestDto());
            return Ok(result);
        }

    }
    
}