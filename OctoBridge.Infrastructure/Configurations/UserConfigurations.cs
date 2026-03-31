using OctoBridge.Domain.Entities;
using OctoBridge.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OctoBridge.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.AccessToken).IsRequired();
        builder.Property(u => u.ProviderId).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.CreatedBy).IsRequired(false);
        builder.Property(u => u.UpdatedAt).IsRequired();
        builder.Property(u => u.UpdatedBy).IsRequired(false);

        builder.HasIndex(u => u.Email).IsUnique();

    }
}