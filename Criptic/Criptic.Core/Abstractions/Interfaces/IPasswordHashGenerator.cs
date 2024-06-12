namespace Criptic.Core.UserControl;

public interface IPasswordHashGenerator
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}