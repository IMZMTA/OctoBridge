namespace OctoBridge.Domain.Enums
{
    /// <summary>
    /// Visibility options for repositories.
    /// </summary>
    public enum RepoVisibility
    {
        /// <summary>
        /// All repositories.
        /// </summary>
        All = 1,

        /// <summary>
        /// Public repositories only.
        /// </summary>
        Public = 2,

        /// <summary>
        /// Private repositories only.
        /// </summary>
        Private = 3
    }

    /// <summary>
    /// Sorting options for repositories.
    /// </summary>
    public enum RepoSort
    {
        /// <summary>
        /// Sort by creation date.
        /// </summary>
        Created = 1,

        /// <summary>
        /// Sort by last updated date.
        /// </summary>
        Updated = 2,

        /// <summary>
        /// Sort by last push date.
        /// </summary>
        Pushed = 3,

        /// <summary>
        /// Sort by full repository name.
        /// </summary>
        FullName = 4
    }

    /// <summary>
    /// Direction of sorting for repositories.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending order.
        /// </summary>
        Asc = 1,

        /// <summary>
        /// Descending order.
        /// </summary>
        Desc = 2
    }

    /// <summary>
    /// Affiliation of the authenticated user to the repository.
    /// </summary>
    public enum RepoAffiliation
    {
        /// <summary>
        /// User is the owner of the repository.
        /// </summary>
        Owner = 1,

        /// <summary>
        /// User is a collaborator.
        /// </summary>
        Collaborator = 2,

        /// <summary>
        /// User is a member of the organization owning the repository.
        /// </summary>
        OrganizationMember = 3
    }

    /// <summary>
    /// Type of repository owner.
    /// </summary>
    public enum RepoOwnerType
    {
        /// <summary>
        /// Authenticated user.
        /// </summary>
        Authenticated = 1,

        /// <summary>
        /// Specific user.
        /// </summary>
        User = 2,

        /// <summary>
        /// Organization account.
        /// </summary>
        Organization = 3
    }

    /// <summary>
    /// Repository type filter.
    /// </summary>
    public enum RepoType
    {
        /// <summary>
        /// All types.
        /// </summary>
        All = 1,

        /// <summary>
        /// Repositories owned by the user.
        /// </summary>
        Owner = 2,

        /// <summary>
        /// Public repositories.
        /// </summary>
        Public = 3,

        /// <summary>
        /// Private repositories.
        /// </summary>
        Private = 4,

        /// <summary>
        /// Repositories the user is a member of.
        /// </summary>
        Member = 5
    }

    public enum IssueState
    {
        /// <summary>
        /// Open issues only.
        /// </summary>
        Open = 1,
        /// <summary>
        /// Closed issues only.
        /// </summary>
        Closed = 2,
        /// <summary>
        /// All issues.
        /// </summary>
        All = 3
    }

    public enum IssueSortType
    {
        /// <summary>
        /// Sort by creation date.
        /// </summary>
        Created = 1,

        /// <summary>
        /// Sort by last updated date.
        /// </summary>
        Updated = 2,

        /// <summary>
        /// Sort by number of comments.
        /// </summary>
        Comments = 3
    }
}