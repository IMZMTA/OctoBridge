using System.Text;
using System.Security.Claims;
using OctoBridge.Domain.Config;
using OctoBridge.Domain.Models;
using Microsoft.AspNetCore.Http;
using OctoBridge.Domain.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace OctoBridge.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly JwtSettings jwtSettings;
    private readonly SymmetricSecurityKey key;

    public TokenService(IOptions<AppSettings> settings)
    {
        jwtSettings = settings.Value.Jwt;
        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
    }

    public string GenerateAccessToken(UserModel user)
    {
        var claims = new List<Claim>
        {
            new(TokenClaims.Id, user.UserId.ToString()),
            new(TokenClaims.ProviderId, user.ProviderId.ToString()),
            new(TokenClaims.Email, user.Email ?? string.Empty),
            new(TokenClaims.UserName, user.UserName ?? string.Empty),
            new(TokenClaims.Type, TokenClaims.AccessToken)
        };

        return BuildToken(user.UserName ?? user.UserId.ToString(), claims, DateTime.UtcNow.AddHours(jwtSettings.ExpirationHours));
    }

    private string BuildToken(string subject, IEnumerable<Claim> claims, DateTime expires)
    {
        var allClaims = new List<Claim>(claims)
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: allClaims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string? ExtractTokenFromHeader(HttpContext context)
    {
        var authHeader = context.Request.Headers[AppConstants.Authorization].ToString();
        return authHeader.StartsWith(AppConstants.Bearer) ? authHeader.Substring(AppConstants.Bearer.Length).Trim() : null;
    }

    public string? ExtractRefreshTokenFromCookie(HttpContext context) =>
        context.Request.Cookies.TryGetValue("refreshToken", out var token) ? token : null;


}
