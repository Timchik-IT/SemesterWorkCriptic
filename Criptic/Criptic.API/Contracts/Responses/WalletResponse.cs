namespace Criptic.API.Contracts.Responses;

public record WalletResponse(
    Guid Id,
    Guid UserId,
    int Balance);