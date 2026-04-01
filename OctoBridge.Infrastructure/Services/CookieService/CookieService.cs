using Microsoft.AspNetCore.Http;
using OctoBridge.Domain.Constants;

namespace OctoBridge.Infrastructure.Services.CookieService;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor context;

    public CookieService(IHttpContextAccessor _context)
    {
        context = _context;
    }
    public void SetJwtCookie(string token)
    {
        context.HttpContext?.Response.Cookies.Append(SecurityConstants.JwtTokenName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(2)
        });
    }
    public void ClearJwtCookie()
    {
        context.HttpContext?.Response.Cookies.Delete(SecurityConstants.JwtTokenName, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

}