using OctoBridge.Domain.Enums;

namespace OctoBridge.Domain.Models.OctoBridgeApp;

public class UserResponseModel
{
    public long? ProviderId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string ProfileUrl { get; set; } = string.Empty;
    public string ConnectionStatus { get; set; } = GitHubConnectionStatus.Connected.ToString();
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
}