using Criptic.Core.Models;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Criptic.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .HasMaxLength(User.MaxNameNameLength)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired();
        
        builder.Property(u => u.ImageData)
            .HasColumnType("bytea");

        builder.Property(u => u.Nfts);
    }
}