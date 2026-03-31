using OctoBridge.Domain.Interfaces;

namespace OctoBridge.Domain.Entities;

public class User : BaseEntity, ITrackEntity
{
    public long ProviderId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }

}
