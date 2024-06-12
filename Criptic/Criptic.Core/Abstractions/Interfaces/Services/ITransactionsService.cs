using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Services;

public interface ITransactionsService
{
    Task<List<Transaction>> GetAllTransactions();
    Task<Guid> CreateTransaction(Transaction transaction);
    Task<Guid> DeleteTransaction(Guid id);
    Task<List<Transaction>> GetWalletTransactions(Guid walletId);
}