using Criptic.Core.Models;

namespace Criptic.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public byte[]? ImageData { get; set; } = Array.Empty<byte>();
    
    public string Name { get; set; } = string.Empty;
    
    public string Role { get; set; } = string.Empty;
     
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public Guid WalletId { get; set; }

    public WalletEntity Wallet { get; set; }

    public ICollection<NftEntity> Nfts { get; set; } = new List<NftEntity>();
    
}
