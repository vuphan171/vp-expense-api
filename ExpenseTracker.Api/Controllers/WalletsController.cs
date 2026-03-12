using ExpenseTracker.Api.Common;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Wallets;
using ExpenseTracker.Application.Features.Wallets.Dtos;
using ExpenseTracker.Domain.Features.Wallets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController(WalletService walletService) : BaseApiController
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<WalletResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<WalletResponseDto>>> Get([FromQuery] PaginationRequest request)
    {
        var wallets = await walletService.GetAllByCustomerAsync(request);
        return Ok(wallets);
    }


    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<Wallet>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Wallet>> Create([FromBody] CreateWalletDto  createWalletDto)
    {
        var result = await walletService.CreateAsync(createWalletDto);
        return Ok(result);
        
    }
}