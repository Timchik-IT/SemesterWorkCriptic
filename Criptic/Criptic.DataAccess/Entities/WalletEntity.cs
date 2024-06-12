using Criptic.Core.Models;

namespace Criptic.DataAccess.Entities;

public class WalletEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public int Sum { get; set; }
    
    public UserEntity User { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}   