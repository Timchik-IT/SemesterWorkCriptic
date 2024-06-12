using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces;
using Criptic.Core.Abstractions.Interfaces.Repositories;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Criptic.Core.UserControl;

namespace Criptic.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHashGenerator _passwordHashGenerator;
    private readonly IJwtProvider _jwtProvider;
    
    public UsersService(IUsersRepository usersRepository, 
        IPasswordHashGenerator passwordHashGenerator,
        IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHashGenerator = passwordHashGenerator;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _usersRepository.Get();
    }
    
    public async Task<Guid> CreateUser(User user)
    {
        var hashedPassword = _passwordHashGenerator.Generate(user.PasswordHash);
        var userWithHashedPassword = new User(user.Id, user.ImageData, user.Name, user.Role, user.Email, hashedPassword);
        _jwtProvider.GenerateToken(userWithHashedPassword);
        
        return await _usersRepository.Create(userWithHashedPassword);
    }

    public async Task<Guid> UpdateUser(Guid id, string name, byte[] imageData)
    {
        return await _usersRepository.Update(id, name, imageData);
    }

    public async Task<Guid> DeleteUser(Guid id)
    {
        return await _usersRepository.Delete(id);
    }

    public async Task<string> Registration(User user)
    {
        try
        {
            var hashedPassword = _passwordHashGenerator.Generate(user.PasswordHash);
            var userWithHashedPassword = new User(user.Id, user.ImageData, user.Name, user.Role, user.Email, hashedPassword);
            await _usersRepository.Create(userWithHashedPassword);
            return userWithHashedPassword.Name;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public async Task<(string token, User user)> LogIn(string password, User user)
    {
        try
        {
            if (!_passwordHashGenerator.Verify(password, user.PasswordHash))
                throw new Exception("Password is incorrect");

            _jwtProvider.GenerateToken(user);
            return (_jwtProvider.GenerateToken(user), user);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<User> GetUserByEmail(string email)
    {
        var data = await _usersRepository.GetUserByEmail(email);
        if (data.error == string.Empty)
            return data.user;
        throw new ArgumentException("There is no such users");
    }

    public async Task<User> GetUserById(Guid id)
    {
        var data = await _usersRepository.GetUserById(id);
        if (data.error == string.Empty)
            return data.user;
        throw new ArgumentException("There is no such users");
    }

    public async Task<bool> IsUserByEmail(string email)
    {
        var data = await _usersRepository.GetUserByEmail(email);
        return data.error == string.Empty;
    }
}