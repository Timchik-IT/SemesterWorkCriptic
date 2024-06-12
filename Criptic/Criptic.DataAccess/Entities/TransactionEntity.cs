namespace Criptic.DataAccess.Entities;

public class TransactionEntity
{
    public Guid Id { get; set; }
    
    public Guid WalletId { get; set; }
    
    public bool Operation { get; set; }
    
    public string Description { get; set; }
    
    public WalletEntity Wallet { get; set; }
}