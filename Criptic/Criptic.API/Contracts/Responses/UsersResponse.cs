namespace Criptic.API.Contracts.Responses;

public record UsersResponse(
    Guid Id,
    string ImageData,
    string Name,
    string Role,
    string Email
);