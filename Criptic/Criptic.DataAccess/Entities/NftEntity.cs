using Criptic.Core.Models;

namespace Criptic.DataAccess.Entities;

public class NftEntity
{
    public Guid Id { get; set; }
    
    public byte[] ImageData { get; set; }
    
    public Guid OwnerId { get; set; }
    
    public string Name { get; set; } = String.Empty;
    
    public decimal Price { get; set; } 
    
    public UserEntity User { get; set; }
}