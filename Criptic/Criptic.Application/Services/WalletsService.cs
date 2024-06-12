using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.ComTypes;
using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Criptic.DataAccess.Entities;

namespace Criptic.Application.Services;

public class WalletsService : IWalletsService
{
    private readonly IWalletRepoisitory _walletRepoisitory;
    private readonly ITransactionsService _transactionsService;

    public WalletsService(IWalletRepoisitory walletRepoisitory, ITransactionsService transactionsService)
    {
        _walletRepoisitory = walletRepoisitory;
        _transactionsService = transactionsService;
    }

    public async Task<List<Wallet>> GetAllWallets()
    {
        return await _walletRepoisitory.Get();
    }

    public async Task<Guid> CreateWallet(Wallet wallet)
    {
        return await _walletRepoisitory.Create(wallet);
    }

    public async Task<Guid> UpdateWallet(Guid id, bool operation, int amount )
    {
        return await _walletRepoisitory.Update(id, operation, amount);
    }

    public async Task<Guid> DeleteWallet(Guid id)
    {
        return await _walletRepoisitory.Delete(id);
    }

    public async Task<Guid> GetWalletByUserId(Guid userId)
    {
        return await _walletRepoisitory.GetWalletByUserId(userId);
    }

    public async Task<Wallet> GetWalletModelByUserId(Guid userId)
    {
        return await _walletRepoisitory.GetWallet(userId);
    }

    public async Task<int> GetBalance(Guid walletId)
    {
        return await _walletRepoisitory.GetBalance(walletId);
    }

    public async Task<string> ReplenishWallet(Guid walletId, decimal amount)
    {
        var description = $"Wallet replenish on {amount}";
        var response = await _walletRepoisitory.ReplenishWallet(walletId, amount);
        await _transactionsService.CreateTransaction(new Transaction(
            new Guid(),
            walletId,
            true,
            description
        ));
        return response;
    }

    public async Task<string> WritingWallet(Guid walletId, decimal amount)
    {
        var description = $"Wallet writing on {amount}";
        var response = await _walletRepoisitory.WritingWallet(walletId, amount);
        await _transactionsService.CreateTransaction(new Transaction(
            new Guid(),
            walletId,
            false,
            description
        ));
        return response;
    }
}