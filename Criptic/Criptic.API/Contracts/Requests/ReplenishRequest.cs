namespace Criptic.API.Contracts.Requests;

public record ReplenishRequest(
    Guid WalletId,
    decimal Amount);