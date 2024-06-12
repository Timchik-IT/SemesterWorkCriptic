namespace Criptic.API.Contracts.Requests;

public record BuyNftRequest(
    Guid NftId,
    Guid NewOwnerId);