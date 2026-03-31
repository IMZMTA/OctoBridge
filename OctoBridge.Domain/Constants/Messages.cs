namespace OctoBridge.Domain.Constants;

public static class Messages
{
    public const string General = "General";
    public const string ValidationFailed = "Validation failed.";
    public const string UnexpectedError = "An unexpected error occurred.";
    public const string InternalServerError = "Internal Server Error.";

    public const string JWTDescription = "Paste your JWT here if testing manually, otherwise Browser sends 'jwt' cookie automatically. Click Authorize to enable the lock icons.";
    public const string MissingAppSetting = "Missing AppSettings configuration.";

    public const string AuthenticationFailed = "Authentication failed";
    public const string UnauthorizedAccess = "Unauthorized access";
    public const string SessionExpired = "Session expired";
    public const string UserOrSessionExpired = "User is not authenticated or session has expired.";
    public const string UserNotFound = "User not found.";
    public const string AccessTokenEmpty = "Access Token cannot be empty.";

    public const string DisconnectSuccess = "Users successfully disconnected from GitHub.";

    #region OAuth Validation
    public const string InvalidProvider = "Invalid authentication provider. Please use GitHub only for now. In future, we will support more providers like Google and LinkedIn.";
    public const string ProviderNotSupported = "Provider '{0}' is not supported.";
    public const string OAuthProviderNotSupported = "Provider '{0}' is not supported for OAuth.";
    public const string OAuthLoginSuccess = "User successfully logged in via OAuth.";
    public const string RedirectUrlGenerated = "Redirect URL generated successfully. Copy the Redirect URL and paste it in your browser. Please note that you may need to log in to your {0} account.";
    public const string LogoutSuccessfully = "User logged out successfully.";

    #endregion

    #region GitHub Validation

    public const string RepositoryOwnerRequired = "Repository owner is required.";
    public const string RepositoryNameRequired = "Repository name is required.";

    public const string IssueTitleRequired = "Issue title is required.";
    public const string IssueTitleTooLong = "Issue title cannot exceed {0} characters.";

    public const string LabelNull = "Label name cannot be null.";
    public const string LabelTooLong = "Label name must not exceed {0} characters.";

    public const string AssigneeNull = "Assignee username cannot be null.";
    public const string AssigneeTooLong = "GitHub username must not exceed {0} characters.";

    public const string IssueBodyNull = "Issue body cannot be null.";
    public const string IssueBodyTooLong = "Issue body cannot exceed {0} characters.";
    public const string IssueCreatedSuccessfully = "Issue created successfully.";

    #endregion

    #region Pull Request Validation


    public const string TitleTooLong = "Title must not exceed {0} characters.";
    public const string TitleRequiredWhenNoIssue = "Title is required when Issue is not provided.";

    public const string IssueMustBePositive = "Issue must be a positive number.";
    public const string EitherTitleOrIssue = "Please provide either a Title or an Issue number, but not both.";

    public const string BodyTooLong = "Body must not exceed {0} characters.";

    public const string HeadRequired = "Head branch is required.";
    public const string HeadInvalid = "Head must be a valid branch name or 'user:branch'.";
    public const string BaseRequired = "Base branch is required.";
    public const string BaseInvalid = "Base must be a valid branch name.";

    public const string HeadRepoRequired = "HeadRepo is required for cross-repository pull requests.";

    public const string PRCreatedSuccessfully = "Pull request created successfully.";

    #endregion

    #region Commits

    public const string CommitsFetchedSuccess = "Commits fetched successfully.";

    #endregion

    #region Commit Validation

    public const string PageSizeInvalid = "Page size must be between 1 and {0}.";
    public const string PageNumberInvalid = "Page number must be greater than or equal to 1.";

    public const string SinceInvalid = "Since must be between 1970 and current UTC time.";
    public const string UntilInvalid = "Until must be between 1970 and current UTC time.";
    public const string SinceUntilInvalid = "Since must be earlier than or equal to Until.";

    public const string AuthorTooLong = "Author username must not exceed {0} characters.";
    public const string CommitterTooLong = "Committer username must not exceed {0} characters.";

    #endregion

    #region Repositories

    public const string RepositoriesFetchedSuccess = "Repositories fetched successfully.";

    #endregion

    #region Repository Validation

    public const string OwnerNameRequired = "Owner name is required when fetching for a specific user or organization.";

    public const string AffiliationsEmpty = "Affiliations cannot be empty.";
    public const string InvalidAffiliation = "Invalid affiliation value.";

    public const string InvalidVisibility = "Invalid visibility option.";
    public const string InvalidType = "Invalid type option.";
    public const string InvalidSort = "Invalid sort option.";
    public const string InvalidDirection = "Invalid direction option.";

    public const string InvalidTypeCombination = "Cannot use 'type' with 'visibility' or 'affiliation'.";

    #endregion

    #region Repository Issues

    public const string RepositoryIssuesFetchedSuccess = "Issues of Repository fetched successfully.";

    #endregion

    #region Repository Issues Validation
    public const string InvalidIssueState = "Invalid issue state.";
    public const string InvalidAssignee = "Assignee must be a valid username, '*', or 'none'.";

    #endregion

    #region GitHub PAT

    public const string PATLoginSuccess = "User logged in successfully.";
    public const string ServerPATLoginSuccess = "User logged in successfully via server PAT.";

    public const string PATRequired = "GitHub Personal Access Token is required.";
    public const string PATTooLong = "Token must not exceed {0} characters.";
    public const string PATInvalidFormat = "Invalid GitHub PAT format. It should start with 'ghp_' or 'github_pat_'.";

    public const string GitHubPATMissingConfig = "GitHub PAT is not configured in application settings.";

    public const string ConnectorNameTooLong = "Connector name must not exceed {0} characters.";

    #endregion

}