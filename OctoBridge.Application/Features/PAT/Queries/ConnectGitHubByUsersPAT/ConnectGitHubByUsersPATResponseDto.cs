namespace OctoBridge.Application.Features.PAT.Queries.ConnectGitHubByUsersPAT;

public class ConnectGitHubByUsersPATResponseDto
{
    public long? ProviderId { get; set; }
    public string Username { get; init; } = string.Empty;
    public string? AvatarUrl { get; init; }
    public string? ProfileUrl { get; init; }
    public string? ConnectionStatus { get; init; }
    public DateTime ConnectedAt { get; init; } = DateTime.UtcNow;
}
