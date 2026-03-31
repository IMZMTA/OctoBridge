namespace OctoBridge.Infrastructure.Services.UserContext;

public interface IUserContext
{
    int UserId { get; }
    long ProviderId { get; }
    string UserName { get; }
}