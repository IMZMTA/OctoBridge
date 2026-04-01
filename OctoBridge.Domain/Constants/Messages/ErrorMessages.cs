namespace OctoBridge.Domain.Constants.Messages;

public static class ErrorMessages
{
    public const string ValidationFailed = "Validation failed.";
    public const string Unexpected = "An unexpected error occurred.";
    public const string InternalServer = "Internal server error.";
    public const string ExternalService = "External service error.";

    public const string AuthenticationFailed = "Authentication failed.";
    public const string UnauthorizedAccess = "Unauthorized access.";
    public const string SessionExpired = "Session expired.";
    public const string UserNotFound = "User not found.";
    public const string UserOrSessionExpired = "User is not authenticated or session has expired. Please Login again";

    public const string UnknownGitHubError = "Unknown GitHub error.";

    public const string MissingAppSettings = "Missing AppSettings configuration.";
    public const string AccessTokenEmpty = "Access token cannot be empty.";

    public const string OAuthErrorOccurred = "An error occurred during OAuth authentication.";
    public const string GitHubPATMissingConfig = "GitHub PAT is not configured in application settings.";
    
}