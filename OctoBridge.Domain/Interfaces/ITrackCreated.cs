namespace OctoBridge.Domain.Interfaces;

public interface ITrackCreated
{
    DateTime CreatedAt { get; set; }
    int? CreatedBy { get; set; }
}
