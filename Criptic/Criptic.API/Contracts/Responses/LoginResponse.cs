namespace Criptic.API.Contracts.Responses;

public record LoginResponse(
    Guid UserId,
    string Token);