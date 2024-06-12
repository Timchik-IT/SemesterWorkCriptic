namespace Criptic.API.Contracts.Requests;

public record UserUpdateRequest(
    string ImageData,
    string Name);