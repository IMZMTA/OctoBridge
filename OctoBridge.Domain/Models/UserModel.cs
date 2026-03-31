namespace OctoBridge.Domain.Models;

public class UserModel
{
    public int UserId { get; set; }
    public long ProviderId { get; set; }
    public string AvatarUrl { get; init; } = string.Empty;
    public string ProfileUrl { get; init; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }

}