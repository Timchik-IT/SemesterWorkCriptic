using Criptic.API.Contracts;
using Criptic.API.Contracts.Requests;
using Criptic.API.Contracts.Responses;
using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.API.Controllers;


/// <summary>
/// Wallet Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class WalletsController : ControllerBase
{
    private readonly IWalletsService _walletService;

    public WalletsController(IWalletsService walletsService)
    {
        _walletService = walletsService;
    }

    /// <summary>
    /// Get all Wallets
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<WalletResponse>>> GetWallets()
    {
        var wallets = await _walletService.GetAllWallets();

        var response = wallets.Select(w => new WalletResponse(w.Id, w.UserId, w.Balance));

        return Ok(response);
    }
    
    [HttpPut("/wallet/replenish")]
    public async Task<ActionResult<string>> ReplenishWallet([FromBody] ReplenishRequest request)
    {
        return Ok(await _walletService.ReplenishWallet(request.WalletId, request.Amount));
    }

    [HttpGet("/wallet/{userId}")]
    public async Task<ActionResult<Guid>> FindWalletByUserId(Guid userId)
    {
        return Ok(await _walletService.GetWalletByUserId(userId));
    }
    
    [HttpGet("/walletModel/{userId}")]
    public async Task<ActionResult<Wallet>> FindWalletModelByUserId(Guid userId)
    {
        return Ok(await _walletService.GetWalletModelByUserId(userId));
    }

    [HttpGet("/wallet/balance/{walletId}")]
    public async Task<ActionResult<int>> GetBalanceByWalletId(Guid walletId)
    {
        return Ok(await _walletService.GetBalance(walletId));
    }

    /// <summary>
    /// Create Wallet 
    /// </summary>
    /// <param name="request to create wallet"></param>
    /// <returns></returns>
    /*
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateWallet([FromBody] WalletRequest request)
    {
        var (wallet, error) = Wallet.Create(
            Guid.NewGuid(),
            request.UserId,
            0);
        
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var walletId = await _walletService.CreateWallet(wallet);

        return Ok(walletId);
    }
    */
    
    ///<summary>
    /// UpdateWallet
    /// Wnen ypu BUY smt you need to set FALSE
    /// When you SOLD or REPLENISH your balance smt you need to set TRUE
    /// </summary>
    /// <param name="id"> User id</param>
    /*
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateWallet(Guid id, [FromBody] WalletRequest request)
    {
        var walletId = await _walletService.UpdateWallet(id, request.Operation, request.Amount);

        return Ok(walletId);
    }
    */
    
    /// <summary>
    /// Delete wallet
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /*
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteWallet(Guid id)
    {
        return Ok(await _walletService.DeleteWallet(id));
    }
    */
}