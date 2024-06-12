using Criptic.API.Contracts;
using Criptic.API.Contracts.Requests;
using Criptic.API.Contracts.Responses;
using Criptic.Core.Abstractions;
using Criptic.Core.Abstractions.Interfaces.Services;
using Criptic.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Criptic.API.Controllers;

/// <summary>
/// Nft Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class NftsController : ControllerBase
{
    private readonly INftsService _nftsService;

    public NftsController(INftsService nftsService)
    {
        _nftsService = nftsService;
    }
    
    /// <summary>
    /// Get all Nfts
    /// </summary>
    /// <returns></returns>
    [HttpGet("/nfts")]
    public async Task<ActionResult<List<NftResponse>>> GetNfts([FromQuery] string search= "")
    {
        var nfts = await _nftsService.GetAllNfts();

        if (!string.IsNullOrEmpty(search))
        {
            nfts = nfts.Where(n => n.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var response = nfts.Select(n => new NftResponse(n.Id, Convert.ToBase64String(n.ImageData), n.Name, n.Price, n.OwnerId)).ToList();

        return Ok(response);
    }

    [HttpGet("/nft/{id}")]
    public async Task<ActionResult<NftResponse>> GetNftById(Guid id)
    {
        var nft = await _nftsService.GetNftById(id);
        var response = new NftResponse(nft.Id, Convert.ToBase64String(nft.ImageData), nft.Name, nft.Price, nft.OwnerId);
        return Ok(response);
    }

    /// <summary>
    /// Create nft
    /// </summary>
    /// <param name="request"> params for creating </param>
    /// <returns></returns>
    [HttpPost ("/createNft")]
    public async Task<ActionResult<Guid>> CreateNft([FromBody] NftRequest request)
    {
        var imageData = Convert.FromBase64String(request.ImageData);
        var (nft, error) = Nft.Create(
            Guid.NewGuid(),
            imageData,
            request.Name,
            request.Price,
            request.CreatorId);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var nftId = await _nftsService.CreateNft(nft, request.CreatorId);
        
        return Ok(nftId);
    }

    /// <summary>
    /// Update Nft
    /// </summary>
    /// <param name="id"> Nft id </param>
    /// <param name="request"> params to update </param>
    /// <param name="ownerId"> owner id </param>
    /// <returns></returns>
    [HttpPut("/updateNft/{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateNft(Guid id, [FromBody] NftRequest request, Guid ownerId)
    {
        var nftId = await _nftsService.UpdateNft(id, request.Name, request.Price, ownerId);

        return Ok(nftId);
    }

    /// <summary>
    /// Delete NFT
    /// </summary>
    /// <param name="id"> nft id </param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteNft(Guid id)
    {
        return Ok(await _nftsService.DeleteNft(id));
    }
    
    /// <summary>
    /// Get list of user nfts
    /// </summary>
    /// <param name="id"> User Id </param>
    /// <returns></returns>
    [HttpGet("/usernfts/{id}")]
    public async Task<ActionResult<List<Nft>>> GetUserNfts(Guid id)
    {
        var nfts = await _nftsService.GetNftsById(id);

        return Ok(nfts);
    }
    
    /// <summary>
    /// Buy nft
    /// </summary>
    /// <param name="request"> params to buy </param>
    /// <returns></returns>
    [HttpPut("/buynft/{nftId}/{newOwnerId}")]
    public async Task<ActionResult<string>> BuyNft(Guid nftId, Guid newOwnerId)
    {
        var response = await _nftsService.BuyNft(nftId, newOwnerId);

        if (response == "Success")
            return Ok(response);
        return BadRequest("You bomjik! Upload money!");
    }
}