namespace OctoBridge.Domain.Enums
{
    /// <summary>
    /// Represents the connection status of a GitHub account.
    /// </summary>
    public enum GitHubConnectionStatus
    {
        /// <summary>
        /// GitHub account is disconnected.
        /// </summary>
        Disconnected = 1,

        /// <summary>
        /// GitHub account is connected.
        /// </summary>
        Connected = 2,

        /// <summary>
        /// Connection to GitHub failed.
        /// </summary>
        Failed = 3,

        /// <summary>
        /// Connection to GitHub has expired.
        /// </summary>
        Expired = 4
    }
}