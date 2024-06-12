using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Models;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace Criptic.DataAccess.Repositories;

public class NftRepository : INftsRepository
{
    private readonly CripticDbContext _context;

    public NftRepository(CripticDbContext context)
    {
        _context = context;
    }

    public async Task<List<Nft>> Get()
    {
        var nftEntities = await _context.Nfts
            .AsNoTracking()
            .ToListAsync();

        var nfts = nftEntities
            .Select(n => Nft.Create(
                n.Id, 
                n.ImageData, 
                n.Name, 
                n.Price, 
                n.OwnerId).Nft)
            .ToList();

        return nfts;
    }

    public async Task<Guid> Create(Nft nft, Guid creatorId)
    {
        var nftEntity = new NftEntity
        {
            Id = nft.Id, 
            ImageData = nft.ImageData,
            Name = nft.Name, 
            Price = nft.Price,
            OwnerId = creatorId
        };

        await _context.Nfts.AddAsync(nftEntity);
        await _context.SaveChangesAsync();

        return nftEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string name, decimal price, Guid ownerId)
    {
        await _context.Nfts
            .Where(n => n.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(n => n.Name, name)
                .SetProperty(n => n.Price, price)
                .SetProperty(n => n.OwnerId, ownerId));
        
        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Nfts
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<List<Nft>> GetUserNfts(Guid id)
    {
        var NftEntities = await _context.Nfts
            .AsNoTracking()
            .ToListAsync();
        
        var nfts = NftEntities
            .Where(n => n.OwnerId == id)
            .Select(n => Nft.Create(n.Id, n.ImageData, n.Name, n.Price, n.OwnerId).Nft)
            .ToList();

        return nfts;
    }

    public async Task<Nft> GetNftById(Guid id)
    {
        var NftEntity = await _context.Nfts
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        
        var (nft, error) = Nft.Create(
            NftEntity.Id,
            NftEntity.ImageData,
            NftEntity.Name,
            NftEntity.Price,
            NftEntity.OwnerId);

        if (string.IsNullOrEmpty(error))
            return nft;
        throw new Exception(error);
    }

    public async Task<Guid> GetOwnerId(Guid NftId)
    {
        var NftEntity = await _context.Nfts
            .AsNoTracking()
            .Where(n => n.Id == NftId)
            .FirstOrDefaultAsync();

        return NftEntity!.OwnerId;
    }
}