using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Models;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Criptic.DataAccess.Repositories;

public class WalletRepoisitory : IWalletRepoisitory
{
    private readonly CripticDbContext _context;

    public WalletRepoisitory(CripticDbContext context)
    {
        _context = context;
    }

    public async Task<List<Wallet>> Get()
    {
        var walletEntities = await _context.Wallets
            .AsNoTracking()
            .ToListAsync();

        var wallets = walletEntities
            .Select(w => Wallet.Create(w.Id, w.UserId, w.Sum).wallet)
            .ToList();

        return wallets;
    }

    public async Task<Guid> Create(Wallet wallet)
    {
        var walletEntity = new WalletEntity
        {
            Id = wallet.Id,
            UserId = wallet.UserId,
            Sum = 0
        };

        await _context.Wallets.AddAsync(walletEntity);
        await _context.SaveChangesAsync();

        return walletEntity.Id;
    }

    public async Task<Guid> Update(Guid id, bool operation, int amount)
    {
        var sum = _context.Wallets
            .FirstOrDefaultAsync(w => w.Id == id).Result!.Sum;

        if (operation)
            sum += amount;
        else
            sum -= amount;
        
        await _context.Wallets
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(w => w.Sum, sum));

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Wallets
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
    
    public async Task<Guid> GetWalletByUserId(Guid id)
    {
        var walletEntity = await _context.Wallets
            .AsNoTracking()
            .Where(w => w.UserId == id)
            .FirstOrDefaultAsync();
        
        return walletEntity!.Id;
    }

    public async Task<Wallet> GetWallet(Guid userId)
    {
        var walletEntity = await _context.Wallets
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .FirstOrDefaultAsync();

        var wallet = Wallet.Create(
            walletEntity!.Id, 
            walletEntity.UserId, 
            walletEntity.Sum).wallet;

        return wallet;
    }

    public async Task<int> GetBalance(Guid id)
    {
        var walletEntity = await _context.Wallets
            .AsNoTracking()
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();
        
        return walletEntity!.Sum;
    }

    public async Task<string> ReplenishWallet(Guid walletId, decimal amount)
    {
        var walletEntity = await _context.Wallets
            .AsNoTracking()
            .Where(w => w.Id == walletId)
            .FirstOrDefaultAsync();
        
        var sum = walletEntity!.Sum + amount;
        
        await _context.Wallets
            .Where(u => u.Id == walletId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(w => w.Sum, sum));
        
        return "Success";
        
    }

    public async Task<string> WritingWallet(Guid walletId, decimal amount)
    {
        var walletEntity = await _context.Wallets
            .AsNoTracking()
            .Where(w => w.Id == walletId)
            .FirstOrDefaultAsync();
        
        var sum = walletEntity!.Sum - amount;

        if (sum < 0)
            return "Refused";
        
        await _context.Wallets
            .Where(u => u.Id == walletId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(w => w.Sum, sum));
        
        return "Success";
    }
}