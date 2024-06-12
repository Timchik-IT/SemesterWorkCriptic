namespace Criptic.Core.UserControl;
using BCrypt.Net;

public class PasswordHashGenerator : IPasswordHashGenerator
{
    public string Generate(string password) =>
        BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.EnhancedVerify(password, hashedPassword);
}
