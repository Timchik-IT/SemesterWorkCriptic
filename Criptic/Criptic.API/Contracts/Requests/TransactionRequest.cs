namespace Criptic.API.Contracts.Requests;

public record TransactionRequest(
    Guid WalletId,
    bool Operation,
    string Description);
    