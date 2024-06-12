using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Criptic.DataAccess;

public class CripticDbContext : DbContext
{
    public CripticDbContext(DbContextOptions<CripticDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<NftEntity> Nfts { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<WalletEntity> Wallets { get; set; }
    
    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserEntity>()
            .HasMany(x => x.Nfts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired();

        builder.Entity<NftEntity>()
            .HasOne(x => x.User)
            .WithMany(x => x.Nfts)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired();

        builder.Entity<WalletEntity>()
            .HasOne(x => x.User)
            .WithOne(x => x.Wallet)
            .HasForeignKey<WalletEntity>(x => x.UserId)
            .IsRequired();

        builder.Entity<WalletEntity>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Wallet)
            .HasForeignKey(x => x.WalletId)
            .IsRequired();
            
            builder.Entity<TransactionEntity>()
            .HasOne(x => x.Wallet)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.WalletId)
            .IsRequired();
    }
}