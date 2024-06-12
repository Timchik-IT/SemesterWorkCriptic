using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Transaction = Criptic.Core.Models.Transaction;

namespace Criptic.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly CripticDbContext _context;

    public TransactionRepository(CripticDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> Get()
    {
        var transactionEntities = await _context.Transactions
            .AsNoTracking()
            .ToListAsync();

        var transactions = transactionEntities
            .Select(t => Transaction.Create(t.Id, t.WalletId, t.Operation, t.Description).Transaction)
            .ToList();

        return transactions;
    }

    public async Task<Guid> Create(Transaction transaction)
    {
        var transactionEntity = new TransactionEntity
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            Operation = transaction.Operation,
            Description = transaction.Description
        };

        await _context.Transactions.AddAsync(transactionEntity);
        await _context.SaveChangesAsync();

        return transactionEntity.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Transactions
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<List<Transaction>> GetUserTransactions(Guid walletId)
    {
        var transactionEntities = await _context.Transactions
            .AsNoTracking()
            .ToListAsync();

        var transactions = transactionEntities
            .Where(t => t.WalletId == walletId)
            .Select(t => Transaction.Create(t.Id, t.WalletId, t.Operation, t.Description).Transaction)
            .ToList();

        return transactions;
    }
}