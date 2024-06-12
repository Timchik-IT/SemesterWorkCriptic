namespace Criptic.Core.Models;

public class User
{
    public const int MaxNameNameLength = 255;

    public User(Guid id, byte[] imageData, string name, string role, string email, string passwordHash)
    {
        Id = id;
        ImageData = imageData;
        Name = name;
        Role = role;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    
    public Guid Id { get; }

    public string Name { get; } = String.Empty;

    public string Role { get; } = string.Empty;

    public string Email { get; } = string.Empty;

    public string PasswordHash { get; } = string.Empty;

    public byte[] ImageData { get; set; }

    public static (User user, string Error) Create(Guid id, byte[] imageData, string name, string role, string email, string passwordHash)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(name) || name.Length > MaxNameNameLength)
        {
            error = "Name error";
        }

        var user = new User(id, imageData, name, role, email, passwordHash);

        return (user, error);
    }
}