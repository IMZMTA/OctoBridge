using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OctoBridge.Domain.Constants;
using OctoBridge.Infrastructure.Exceptions;

namespace OctoBridge.Infrastructure.Services.UserContext;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    public UserContext(IHttpContextAccessor _httpContextAccessor)
    {
        httpContextAccessor = _httpContextAccessor;
    }

    private ClaimsPrincipal User
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
            {
                throw new AuthenticationException(Messages.UserOrSessionExpired, AppConstants.Session);
            }
            return user ?? throw new UnauthorizedAccessException("No HttpContext");
        }
    }

    public int UserId
    {
        get
        {
            var value = User.FindFirst(TokenClaims.Id)?.Value;

            if (!int.TryParse(value, out var userId))
            {
                throw new AuthenticationException("Invalid or missing User ID in session.", "UserId");
            }

            return userId;
        }
    }

    public long ProviderId
    {
        get
        {
            var value = User.FindFirst(TokenClaims.ProviderId)?.Value;

            if (!long.TryParse(value, out var providerId))
            {
                throw new AuthenticationException("Invalid provider identification in session.", "ProviderId");
            }

            return providerId;
        }
    }

    public string UserName
    {
        get
        {
            return User.FindFirst(TokenClaims.UserName)?.Value
                ?? throw new AuthenticationException("Invalid or missing Username in session.", "UserName");
        }
    }
}