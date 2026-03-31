using OctoBridge.Domain.Entities;

namespace OctoBridge.Infrastructure.Repository.UserRepository;

public interface IUserRepository
{
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByProviderIdOrEmailAsync(long providerId, string email, CancellationToken ct);
    Task<User?> GetByIdAndProviderIdAsync(int userId, long providerId, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);
    Task<User?> RemoveByIdAsync(int userId, CancellationToken cancellationToken);
    Task<User?> RemoveUserAsync(int userId, long providerId, CancellationToken cancellationToken);
    Task<User?> UpdateAsync(User user, CancellationToken cancellationToken);
}