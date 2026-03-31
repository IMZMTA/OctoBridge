using OctoBridge.Domain.Constants;
using Microsoft.AspNetCore.Http;

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
        context.HttpContext?.Response.Cookies.Append(AppConstants.JWTTokenName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(2)
        });
    }
    public void ClearJwtCookie()
    {
        context.HttpContext?.Response.Cookies.Delete(AppConstants.JWTTokenName, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

}