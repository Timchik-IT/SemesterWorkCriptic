namespace Criptic.API.Contracts.Responses;

public record NftResponse(
    Guid Id,
    string ImageData,
    string Name,
    decimal Price,
    Guid OwnerId);