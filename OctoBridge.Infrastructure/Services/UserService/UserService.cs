using OctoBridge.Domain.Models;
using OctoBridge.Domain.Entities;
using OctoBridge.Domain.Constants;
using Microsoft.Extensions.Caching.Memory;
using OctoBridge.Infrastructure.Services.Security;
using OctoBridge.Infrastructure.Repository.UserRepository;

namespace OctoBridge.Infrastructure.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IEncryptionService encryption;
    private readonly IMemoryCache cache;
    public UserService(IUserRepository _userRepository, IEncryptionService _encryption, IMemoryCache _cache)
    {
        cache = _cache;
        encryption = _encryption;
        userRepository = _userRepository;
    }

    public async Task<int> RemoveUserAsync(int userId, long providerId, CancellationToken cancellationToken)
    {
        var user = await userRepository.RemoveUserAsync(userId, providerId, cancellationToken);
        if (user == null)
        {
            return 0;
        }
        return user.Id;
    }

    public async Task<UserModel?> UpsertAsync(UserModel userModel, CancellationToken cancellationToken)
    {
        var encryptedToken = encryption.Encrypt(userModel.AccessToken);

        var existingUser = await userRepository.GetByProviderIdOrEmailAsync(userModel.ProviderId, userModel.Email, cancellationToken);

        if (existingUser == null)
        {
            var newUser = new User
            {
                ProviderId = userModel.ProviderId,
                UserName = userModel.UserName,
                Email = userModel.Email,
                AccessToken = encryptedToken,
                CreatedAt = DateTime.UtcNow
            };

            var created = await userRepository.CreateAsync(newUser, cancellationToken);
            return MapToModel(created);
        }

        existingUser.AccessToken = encryptedToken;
        existingUser.UserName = userModel.UserName;
        existingUser.Email = userModel.Email;
        existingUser.UpdatedAt = DateTime.UtcNow;

        var updateUser = await userRepository.UpdateAsync(existingUser, cancellationToken);
        if (updateUser == null)
        {
            return null;
        }
        return MapToModel(updateUser);
    }

    public async Task<UserModel?> GetByIdAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
        {
            return null;
        }

        return MapToModel(user);
    }

    public async Task<UserModel?> GetByIdAndProviderIdAsync(int userId, long providerId, CancellationToken cancellationToken)
    {
        var cacheKey = $"token_{userId}_{providerId}";
        if(!cache.TryGetValue(cacheKey, out string? token))
        {
            var user = await userRepository.GetByIdAndProviderIdAsync(userId, providerId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            token = encryption.Decrypt(user.AccessToken);

            cache.Set(cacheKey, token, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SecurityConstants.CacheExpirationInMinutes),
                SlidingExpiration = TimeSpan.FromMinutes(SecurityConstants.CacheSlidingExpirationInMinutes),
                Size = SecurityConstants.CacheSize
            });

        }

        return new UserModel
        {
            UserId = userId,
            ProviderId = providerId,
            AccessToken = token ?? string.Empty
        };

    }

    private static UserModel MapToModel(User user)
    {

        return new UserModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            ProviderId = user.ProviderId
        };
    }

}
