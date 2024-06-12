using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Services;

public interface IUsersService
{
    Task<List<User>> GetAllUsers();
    Task<Guid> CreateUser(User user);
    Task<Guid> UpdateUser(Guid id, string name, byte[] imageData);
    Task<Guid> DeleteUser(Guid id);
    Task<string> Registration(User user);
    Task<(string token, User user)> LogIn(string password, User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserById(Guid id);
    Task<bool> IsUserByEmail(string email);
}
