using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Repositories;

public interface INftsRepository
{
    Task<List<Nft>> Get();
    Task<Guid> Create(Nft nft, Guid creatorId);
    Task<Guid> Delete(Guid id);
    Task<Guid> Update(Guid id, string name, decimal price, Guid ownerId);
    Task<Nft> GetNftById(Guid ig);
    Task<List<Nft>> GetUserNfts(Guid id);
    Task<Guid> GetOwnerId(Guid NftId);
}