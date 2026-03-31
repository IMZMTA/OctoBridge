namespace OctoBridge.Domain.Models;

public class TokenModel
{
    public int? UserId { get; set; }
    public long? ProviderId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? TokenType { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? IssuedAt { get; set; }
}
