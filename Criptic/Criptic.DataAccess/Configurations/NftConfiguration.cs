using Criptic.Core.Models;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Criptic.DataAccess.Configurations;

public class NftConfiguration : IEntityTypeConfiguration<NftEntity> 
{
    public void Configure(EntityTypeBuilder<NftEntity> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.ImageData)
            .HasColumnType("bytea");

        builder.Property(n => n.Name)
            .HasMaxLength(Nft.MaxNameNftLength)
            .IsRequired();

        builder.Property(n => n.Price)
            .IsRequired();

        builder.Property(n => n.OwnerId)
            .IsRequired();
    }
}