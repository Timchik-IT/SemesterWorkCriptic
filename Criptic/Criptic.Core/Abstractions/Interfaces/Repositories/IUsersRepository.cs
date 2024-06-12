using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<List<User>> Get();
    Task<Guid> Create(User user);
    Task<Guid> Update(Guid id, string name, byte[] imageData);
    Task<Guid> Delete(Guid id);
    Task<(User user, string error)> GetUserById(Guid id);
    Task<(User user, string error)> GetUserByEmail(string email);
}
