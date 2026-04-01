namespace OctoBridge.Domain.Enums;

public enum ErrorCode
{
    ValidationFailed = 1,
    ResourceNotFound = 2,
    AuthenticationFailed = 3,
    AuthorizationFailed = 4,
    ConflictDetected = 5,
    InvalidRequest = 6,
    ExternalServiceFailure = 7,
    InternalServerError = 8,
    OAuthFailed = 9
}