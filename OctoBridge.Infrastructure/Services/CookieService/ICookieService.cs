namespace OctoBridge.Infrastructure.Services.CookieService;

public interface ICookieService
{
    void SetJwtCookie(string token);
    void ClearJwtCookie();

}
