using System.Threading.Channels;
using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;

namespace Criptic.Application.Services;

public class NftsService : INftsService
{
    private readonly INftsRepository _nftsRepository;
    private readonly IWalletsService _walletsService;

    public NftsService(INftsRepository nftsRepository, 
        IWalletsService walletService)
    {
        _nftsRepository = nftsRepository;
        _walletsService = walletService;
    }

    public async Task<List<Nft>> GetAllNfts()
    {
        return await _nftsRepository.Get();
    }

    public async Task<Guid> CreateNft(Nft nft, Guid creatorId)
    {
        return await _nftsRepository.Create(nft, creatorId);
    }

    public async Task<Guid> UpdateNft(Guid id, string name, decimal price, Guid ownerId)
    {
        return await _nftsRepository.Update(id, name, price, ownerId);
    }

    public async Task<Guid> DeleteNft(Guid id)
    {
        return await _nftsRepository.Delete(id);
    }

    public async Task<List<Nft>> GetNftsById(Guid id)
    {
        return await _nftsRepository.GetUserNfts(id);
    }

    public async Task<Nft> GetNftById(Guid id)
    {
        return await _nftsRepository.GetNftById(id);
    }

    public async Task<string> BuyNft(Guid nftId, Guid newOwnerId)
    {
        var nft = await _nftsRepository.GetNftById(nftId);
        var sellerId = await _nftsRepository.GetOwnerId(nftId);
        
        var buyerWalletId = await _walletsService.GetWalletByUserId(newOwnerId);
        var sellerWalletId = await _walletsService.GetWalletByUserId(sellerId);

        var buyerResponse = await _walletsService.WritingWallet(buyerWalletId, nft.Price);
        switch (buyerResponse)
        {
            case "Success":
                await _walletsService.ReplenishWallet(sellerWalletId, nft.Price);
                await _nftsRepository.Update(nft.Id, nft.Name, nft.Price + 100, newOwnerId);
                return buyerResponse;
            case "Refused":
                return buyerResponse;
            case "Unknown":
                return buyerResponse;
        }

        return "Unknown";
    }
}