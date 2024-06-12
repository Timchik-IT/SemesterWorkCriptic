using Criptic.API.Contracts;
using Criptic.API.Contracts.Requests;
using Criptic.API.Contracts.Responses;
using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.API.Controllers;


/// <summary>
/// User Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IWalletsService _walletsService;

    public UsersController(IUsersService usersService, IWalletsService walletsService)
    {
        _usersService = usersService;
        _walletsService = walletsService;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet("/users")]
    public async Task<ActionResult<List<UsersResponse>>> GetUsers()
    {
        var users = await _usersService.GetAllUsers();

        var response = users.Select(u => new UsersResponse(u.Id, Convert.ToBase64String(u.ImageData),u.Name, u.Role, u.Email));

        return Ok(response);
    }

    [HttpGet("/users/{id}")]
    public async Task<ActionResult<UsersResponse>> GetUserById(Guid id)
    {
        var user = await _usersService.GetUserById(id);
        var response = new UsersResponse(user.Id, Convert.ToBase64String(user.ImageData), user.Name, user.Role, user.Email);
        return Ok(response);
    }

    /// <summary>
    /// Create user and wallet for him
    /// </summary>
    /// <param name="request"> params for creating user</param>
    /// <param name="role"> role for creating user</param>
    /// <returns></returns>
    [HttpPost("/users/create")]
    public async Task<ActionResult<Guid>> CreateUser([FromBody] UsersRequest request)
    {
        var id = Guid.NewGuid();
        var imageData = Convert.FromBase64String(request.ImageData);
        
        var (user, userError) = Core.Models.User.Create(
            id,
            imageData,
            request.Name,
            request.Role,
            request.Email,
            request.Password);
        
        if (!string.IsNullOrEmpty(userError))
        {
            return BadRequest(userError);
        }

        var userId = await _usersService.CreateUser(user);
        
        var (wallet, walletError) = Wallet.Create(
            new Guid(),
            id,
            0);
        
        if (!string.IsNullOrEmpty(walletError))
        {
            return BadRequest(walletError);
        }
        
        var walletId = await _walletsService.CreateWallet(wallet);
        
        return Ok(userId);
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="id"> user id</param>
    /// <param name="request"> params to update</param>
    /// <returns></returns>
    [HttpPut("/users/update/{id}")]
    public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
    {
        var imageData = Convert.FromBase64String(request.ImageData);
        var userId = await _usersService.UpdateUser(id, request.Name, imageData);
        return Ok(userId);
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id"> id to deleting users</param>
    /// <returns></returns>
    [HttpDelete("/users/delete/{id}")]
    public async Task<ActionResult<Guid>> DeleteUser(Guid id)
    {
        return Ok(await _usersService.DeleteUser(id));
    }
}