using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Repositories;

public interface IWalletRepoisitory
{
    Task<List<Wallet>> Get();
    Task<Guid> Create(Wallet wallet);
    Task<Guid> Update(Guid id, bool operation, int amount);
    Task<Guid> Delete(Guid id);
    Task<Guid> GetWalletByUserId(Guid id);
    Task<int> GetBalance(Guid id);
    Task<Wallet> GetWallet(Guid userId);
    Task<string> ReplenishWallet(Guid walletId, decimal amount);
    Task<string> WritingWallet(Guid walletId, decimal amount);
}