namespace Criptic.API.Contracts.Requests;

public record UsersRequest(
    string ImageData,
    string Name,
    string Role,
    string Email,
    string Password
);