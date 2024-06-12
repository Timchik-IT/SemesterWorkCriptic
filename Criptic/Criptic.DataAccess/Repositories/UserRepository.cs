using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Models;
using Criptic.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace Criptic.DataAccess.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly CripticDbContext _context;

    public UserRepository(CripticDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> Get()
    {
        var userEntities = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        var users = userEntities
            .Select(u => User.Create(
                u.Id,
                u.ImageData,
                u.Name,
                u.Role,
                u.Email,
                u.PasswordHash).user)
            .ToList();

        return users;
    }

    public async Task<Guid> Create(User user)
    {
        var userEntity = new UserEntity
        {
            Id = user.Id,
            Role = user.Role,
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return userEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string name, byte[] imageData)
    {
        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.ImageData, imageData));

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<(User user, string error)> GetUserByEmail(string email)
    {
        var userEntity = await _context.Users
            .Where(x => x.Email == email)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userEntity == null)
            return (null, "User not found!")!;
        return (new User(
            userEntity.Id,
            userEntity.ImageData,
            userEntity.Name,
            userEntity.Role,
            userEntity.Email,
            userEntity.PasswordHash), string.Empty);
    }

    public async Task<(User user, string error)> GetUserById(Guid id)
    {
        var userEntity = await _context.Users
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userEntity == null)
            return (null, "User not found!")!;
        return (new User(
            userEntity.Id,            
            userEntity.ImageData,
            userEntity.Name,
            userEntity.Role,
            userEntity.Email,
            userEntity.PasswordHash), string.Empty); 
    }
}