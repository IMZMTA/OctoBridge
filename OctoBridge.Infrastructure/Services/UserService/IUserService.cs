using OctoBridge.Domain.Models;

namespace OctoBridge.Infrastructure.Services.UserService;

public interface IUserService
{
    Task<UserModel?> UpsertAsync(UserModel user, CancellationToken cancellationToken);
    Task<UserModel?> GetByIdAsync(int userId, CancellationToken cancellationToken);
    Task<int> RemoveUserAsync(int userId, long providerId, CancellationToken cancellationToken);
    Task<UserModel?> GetByIdAndProviderIdAsync(int userId, long providerId, CancellationToken cancellationToken);

}
