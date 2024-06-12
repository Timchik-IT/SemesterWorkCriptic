using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;

namespace Criptic.Application.Services;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionsService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        return await _transactionRepository.Get();
    }

    public async Task<Guid> CreateTransaction(Transaction transaction)
    {
        return await _transactionRepository.Create(transaction);
    }

    public async Task<Guid> DeleteTransaction(Guid id)
    {
        return await _transactionRepository.Delete(id);
    }

    public async Task<List<Transaction>> GetWalletTransactions(Guid walletId)
    {
        return await _transactionRepository.GetUserTransactions(walletId);
    }
}