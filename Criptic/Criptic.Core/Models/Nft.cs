namespace Criptic.Core.Models;

public class Nft
{
    public const int MaxNameNftLength = 255;
    
    public Nft(Guid id, byte[] imageData, string name, decimal price, Guid ownerId)
    {
        Id = id;
        ImageData = imageData;
        Name = name;
        Price = price;
        OwnerId = ownerId;
    }
    
    public Guid Id { get; }
    
    public Guid OwnerId { get; }
    
    public string Name { get; } = string.Empty;
    
    public decimal Price { get; }
    
    public byte[] ImageData { get; set; }
    

    public static (Nft Nft, string Error) Create(Guid id, byte[] imageData, string name, decimal price, Guid ownerId)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > MaxNameNftLength )
        {
            error = "Name error";
        }

        var nft = new Nft(id, imageData, name, price, ownerId);

        return (nft, error);
    }
}