using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> Get();
    Task<Guid> Create(Transaction transaction);
    Task<Guid> Delete(Guid id);
    Task<List<Transaction>> GetUserTransactions(Guid walletId);
}