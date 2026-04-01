namespace OctoBridge.Domain.Constants.Messages;

public static class ValidationMessages
{
    public const string Required = "{0} is required.";
    public const string TooLong = "{0} must not exceed {1} characters.";
    public const string NotNull = "{0} name cannot be null or empty.";

    public const string MustBePositive = "{0} must be a positive number.";
    public const string InvalidProvider = "Invalid authentication provider.";
    public const string InvalidIssueState = "Invalid issue state.";
    public const string InvalidCombination = "Please provide either {0} or {1}, but not both.";

    public const string SinceInvalid = "Since must be between 1970 and current UTC time."; public const string UntilInvalid = "Until must be between 1970 and current UTC time.";

    public const string InvalidDateRange = "{0} must be earlier than or equal to {1}.";
    public const string PageSizeInvalid = "Page size must be between 1 and {0}.";
    public const string PageNumberInvalid = "Page number must be greater than or equal to 1.";

    public const string Invalid = "{0} is invalid.";

    public const string ProviderNotSupported = "Provider '{0}' is not supported.";

    public const string OAuthProviderNotSupported = "Provider '{0}' is not supported for OAuth.";

    public const string TitleRequiredWhenNoIssue = "Title is required when Issue is not provided.";

    public const string HeadInvalid = "Head must be a valid branch name or 'user:branch'.";

    public const string BaseInvalid = "Base must be a valid branch name.";

    public const string HeadRepoRequired = "HeadRepo is required for cross-repository pull requests.";

    public const string SinceUntilInvalid = "Since must be earlier than or equal to Until.";

    public const string OwnerNameRequired = "Owner name is required when fetching for a specific user or organization.";

    public const string AffiliationsEmpty = "Affiliations cannot be empty.";

    public const string InvalidAffiliation = "Invalid affiliation value.";

    public const string InvalidVisibility = "Invalid visibility option.";

    public const string InvalidType = "Invalid type option.";

    public const string InvalidSort = "Invalid sort option.";

    public const string InvalidDirection = "Invalid direction option.";

    public const string InvalidTypeCombination = "Cannot use 'type' with 'visibility' or 'affiliation'.";

    public const string InvalidAssignee = "Assignee must be a valid username, '*', or 'none'.";

    public const string InvalidValue = "Invalid value.";

    public const string UnknownField = "Unknown field.";

    public const string PATInvalidFormat = "Invalid GitHub PAT format. It should start with 'ghp_' or 'github_pat_'.";

    public const string ConnectorNameTooLong = "Connector name must not exceed {0} characters.";
    public const string PATRequired = "GitHub Personal Access Token is required.";
}