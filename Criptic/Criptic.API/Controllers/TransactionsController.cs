using Criptic.API.Contracts.Requests;
using Criptic.API.Contracts.Responses;
using Criptic.Application.Services;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.API.Controllers;


/// <summary>
/// Transaction Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    /// <summary>
    /// Get All Transactions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<TransactionResponse>>> GetTransactions()
    {
        var transactions = await _transactionsService.GetAllTransactions();

        var response = transactions.Select(t =>
            new TransactionResponse(t.Id, t.Operation ? "Replenishment" : "Withdrawal", t.Description));
        
        return Ok(response);
    }

    /// <summary>
    /// Create transaction
    /// </summary>
    /// <param name="request"> params for creating</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTransaction([FromBody] TransactionRequest request)
    {
        var (transaction, error) = Transaction.Create(
            Guid.NewGuid(),
            request.WalletId,
            request.Operation,
            request.Description);
        
        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var transactionId = await _transactionsService.CreateTransaction(transaction);

        return Ok(transactionId);
    }
    
    /// <summary>
    /// Delete transaction
    /// </summary>
    /// <param name="id"> transaction id </param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteNft(Guid id)
    {
        return Ok(await _transactionsService.DeleteTransaction(id));
    }

    /// <summary>
    ///  Get all wallet transactions
    /// </summary>
    /// <param name="walletId"> walletId</param>
    /// <returns></returns>
    [HttpGet("/transactions/{walletId}")]
    public async Task<ActionResult<List<Transaction>>> GetWalletTransactions(Guid walletId)
    {
        var transactions = await _transactionsService.GetWalletTransactions(walletId);

        return Ok(transactions);
    }
}
