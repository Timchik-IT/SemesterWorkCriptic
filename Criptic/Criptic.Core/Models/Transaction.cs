namespace Criptic.Core.Models;

public class Transaction
{
    public Transaction(Guid id, Guid walletId, bool operation, string description)
    {
        Id = id;
        WalletId = walletId;
        Operation = operation;
        Description = description;
    }

    public Guid Id { get; }
    
    public Guid WalletId { get; }
    
    public bool Operation { get; }

    public string Description { get; } = string.Empty;

    public static (Transaction Transaction, string Error) Create(Guid id, Guid walletId, bool operation, string description)
    {
        var error = string.Empty;

        // валлидации
        if (false)
        {
            error = "Error of transaction create";
        }

        var transaction = new Transaction(id, walletId, operation, description);

        return (transaction, error);
    }
}