namespace Criptic.API.Contracts.Requests;

public record RegistrationRequest(
    string Role,
    string Name,
    string Email,
    string Password
    );