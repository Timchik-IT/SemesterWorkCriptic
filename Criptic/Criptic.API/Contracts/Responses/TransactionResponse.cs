namespace Criptic.API.Contracts.Responses;

public record TransactionResponse(
    Guid Id,
    string Operation,
    string Description
    );
    