namespace OctoBridge.Domain.Constants;

public static class AppConstants
{
    public const string AppName = "OctoBridge - GitHub Connector API";
    public const string Version = "v1";
    public const string DefaultCorsPolicy = "DefaultCorsPolicy";
    public const string AppSettings = "AppSettings";
    public const string DefaultConnection = "DefaultConnection";

    public const string Session = "Session";
    public const string Bearer = "Bearer";
    public const string JWT = "JWT";
    public const string JWTTokenName = "OctoBridge_JWT";
    public const string Authorization = "Authorization";
    public const string AntiforgeryHeader = "X-XSRF-TOKEN";
    public const string CacheControlValue = "no-store, no-cache, must-revalidate";
    public const string PragmaValue = "no-cache";
    public const string HeaderExpiresValue = "0";

    public const string AccessToken = "Access Token";
    public const string RefreshToken = "Refresh Token";
    public const string ConnectorName = "OctoBridge-App";

    public const string Space = " ";
    public const string ForwardSlash = "/";
    public const string Underscore = "_";
    public const string Hyphen = "-";

    public const int Zero = 0;
    public const int One = 1;
    public const int Two = 2;
    public const int Three = 3;
    public const int Five = 5;
    public const int Seven = 5;
    public const int MaxLabelNameLength = 50;
    public const int MaxUsernameLength = 39;
    public const int MaxTokenLength = 255;
    public const int MaxIssueTitleLength = 256;
    public const int MaxIssueBodyLength = 65536;
    public const int MaxBranchNameLength = 100;

    public const int MaxRetryAttempts = 3;
    public const int RetryDelayInSecond = 1;
    public const int MaxRateLimit = 2;
    public const int RateLimitIntervalInSeconds = 5;
    public const int CacheExpirationInMinutes = 5;
    public const int CacheSlidingExpirationInMinutes = 2;
    public const int CacheSize = 1;
    public const int CacheSizeLimit = 1000;

    public const int DefaultJwtExpirationMinutes = 120;
    public const int DefaultJwtExpirationHours = 2; 
    public const int DefaultJwtExpirationDays = 7;

}
