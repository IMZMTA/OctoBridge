using Microsoft.AspNetCore.Http;
using OctoBridge.Domain.Models;

namespace OctoBridge.Infrastructure.Services.TokenService;

public interface ITokenService
{
    string GenerateAccessToken(UserModel user);
    string? ExtractTokenFromHeader(HttpContext httpContext);
    string? ExtractRefreshTokenFromCookie(HttpContext httpContext);

}
