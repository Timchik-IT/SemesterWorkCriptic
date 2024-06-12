using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Services;

public interface IWalletsService
{
    Task<List<Wallet>> GetAllWallets();
    Task<Guid> CreateWallet(Wallet wallet);
    Task<Guid> UpdateWallet(Guid id, bool operation, int amount);
    Task<Guid> DeleteWallet(Guid id);
    Task<int> GetBalance(Guid walletId);
    Task<Guid> GetWalletByUserId(Guid userId);
    Task<Wallet> GetWalletModelByUserId(Guid userId);
    Task<string> ReplenishWallet(Guid userId, decimal amount);
    Task<string> WritingWallet(Guid walletId, decimal amount);
}