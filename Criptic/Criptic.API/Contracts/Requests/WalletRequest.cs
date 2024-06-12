namespace Criptic.API.Contracts.Requests;

public record WalletRequest(
    Guid UserId,
    bool Operation,
    int Amount);