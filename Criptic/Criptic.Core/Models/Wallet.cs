namespace Criptic.Core.Models;

public enum Currency
{
    Rub = 0,
    Eur = 1, 
    Usd= 2
}

public class Wallet
{
    public Wallet(Guid id, Guid ownerId, int balance)
    {
        Id = id;
        UserId = ownerId;
        Balance = balance;
    }

    public Guid Id { get; }
    
    public Guid UserId { get; }

    //public Currency Currency { get; } = Currency.Rub;
    // RUBLES ONLY!

    public int Balance { get; }

    public static (Wallet wallet, string error) Create(Guid id, Guid ownerId, int sum)
    {
        var error = string.Empty;

        // валидации

        var wallet = new Wallet(id, ownerId, sum);

        return (wallet, error);
    }
}