namespace OctoBridge.Domain.Constants;

public static class SecurityConstants
{
    public const string Bearer = "Bearer";
    public const string Jwt = "JWT";

    public const string AuthorizationHeader = "Authorization";
    public const string AntiforgeryHeader = "X-XSRF-TOKEN";

    public const string JwtTokenName = "OctoBridge_JWT";

    public const string AccessToken = "AccessToken";
    public const string RefreshToken = "RefreshToken";

    public const string JWTDescription = "Paste your JWT here if testing manually, otherwise Browser sends 'jwt' cookie automatically. Click Authorize to enable the lock icons.";

    public const int CacheExpirationInMinutes = 5;
    public const int CacheSlidingExpirationInMinutes = 2; 
    public const int CacheSize = 1; 
    public const int CacheSizeLimit = 1000;

}