using Microsoft.EntityFrameworkCore;
using OctoBridge.Domain.Entities;
using OctoBridge.Infrastructure.Interfaces;

namespace OctoBridge.Infrastructure.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> GetByProviderIdOrEmailAsync(long providerId, string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.ProviderId == providerId || u.Email == email,
            cancellationToken);

        return user;
    }

    public async Task<User?> GetByIdAndProviderIdAsync(int userId, long providerId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.ProviderId == providerId || u.Id == userId,
            cancellationToken);

        return user;
    }

    public async Task<User?> RemoveByIdAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(userId, cancellationToken);

        if (user == null) return null;
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> RemoveUserAsync(int userId, long providerId, CancellationToken cancellationToken)
    {
        var user = await GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);
        if (user == null) return null;
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

}