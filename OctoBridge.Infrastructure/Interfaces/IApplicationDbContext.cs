using Microsoft.EntityFrameworkCore;
using OctoBridge.Domain.Entities;

namespace OctoBridge.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
