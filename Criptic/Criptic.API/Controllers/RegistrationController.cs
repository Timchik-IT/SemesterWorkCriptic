using System.Security.Claims;
using Criptic.API.Contracts;
using Criptic.API.Contracts.Requests;
using Criptic.API.Contracts.Responses;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.API.Controllers;


/// <summary>
/// Registration controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IWalletsService _walletsService;

    public RegistrationController(IUsersService usersService, IWalletsService walletsService)
    {
        _usersService = usersService;
        _walletsService = walletsService;
    }

    /// <summary>
    /// Registration
    /// </summary>
    /// <param name="request"> params to create user </param>
    /// <param name="role"> role to create user </param>
    /// <returns></returns>
    [HttpPost ("/register")]
    public async Task<ActionResult<string>> Registration([FromBody] RegistrationRequest request)
    {
        try
        {
            var isEmailBusy = await _usersService.IsUserByEmail(request.Email);
            if (isEmailBusy)
                return BadRequest("Email is busy");
            
            var id = Guid.NewGuid();
            var (user, userError) = Core.Models.User.Create(
                id,
                new byte[0],
                request.Name,
                request.Role,
                request.Email,
                request.Password);
        
            if (!string.IsNullOrEmpty(userError))
                return BadRequest(userError);
            var response = await _usersService.Registration(user);
            
            var (wallet, walletError) = Wallet.Create(
                new Guid(),
                id,
                0);
            var walletId = await _walletsService.CreateWallet(wallet);
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    /// <summary>
    /// LogIn
    /// </summary>
    /// <param name="request"> params for login user </param>
    /// <returns></returns>
    [HttpPost ("/login")]
    public async Task<ActionResult<LoginResponse>> LogIn(LoginRequest request)
    {
        try
        {
            if (!await _usersService.IsUserByEmail(request.Email))
                return BadRequest("User with such email not found!");

            var user = await _usersService.GetUserByEmail(request.Email);
            var login = await _usersService.LogIn(request.Password, user);

            Claim[] claims =
            {
                new Claim(user.Role, "true"),
                new Claim("userId", user.Id.ToString())
            };

            foreach (var claim in claims)
                HttpContext.User.Claims.Append(claim);
            return Ok(
                new LoginResponse(
                    user.Id,
                    login.token)
            );
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}