using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Models.GitHub;
using OctoBridge.Domain.Constants.Messages;

namespace OctoBridge.Domain.Common.Helpers;

public static class GitHubErrorMapper
{
    /// <summary>
    /// Converts a GitHubApiError into a list of ApiError suitable for your API response.
    /// Handles multiple errors if GitHub provides them.
    /// </summary>
    public static List<ApiError> MapToApiErrors(GitHubApiError error)
    {
        var errors = new List<ApiError>();

        if (error.Errors != null && error.Errors.Any())
        {
            return error.Errors.Select(err => new ApiError
            {
                Field = err.Field ?? Messages.General,
                Message = $"{error.Message} ({err.Field ?? ValidationMessages.UnknownField})",
                Code = err.Code ?? ErrorCode.ExternalServiceFailure.ToString(),
                Source = ErrorSource.GitHub.ToString()
            }).ToList();
        }

        return new List<ApiError>
        {
            new()
            {
                Field = Messages.General,
                Message = error.Message,
                Code = ErrorCode.ExternalServiceFailure.ToString(),
                Source = ErrorSource.GitHub.ToString()
            }
        };

    }

    public static List<ApiError> MapOAuthError(GitHubOAuthError error)
    {
        return new List<ApiError>
        {
            new ApiError
            {
                Field = Messages.General,
                Message = error.ErrorDescription ?? ErrorMessages.OAuthErrorOccurred,
                Code = error.Error ?? ErrorCode.OAuthFailed.ToString(),
                Source = ErrorSource.GitHub.ToString(),
            }
        };
    }
}