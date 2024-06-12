using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Services;

public interface INftsService
{
    Task<List<Nft>> GetAllNfts();
    Task<Guid> CreateNft(Nft nft, Guid creatorId);
    Task<Guid> UpdateNft(Guid id, string name, decimal price, Guid ownerId);
    Task<Guid> DeleteNft(Guid id);
    Task<Nft> GetNftById(Guid id);
    Task<List<Nft>> GetNftsById(Guid id);
    Task<string> BuyNft(Guid nftId, Guid newOwnerId);
}