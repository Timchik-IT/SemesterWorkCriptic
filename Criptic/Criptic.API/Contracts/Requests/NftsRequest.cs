namespace Criptic.API.Contracts.Requests;

public record NftRequest(
    string ImageData,
    string Name,
    decimal Price,
    Guid CreatorId);