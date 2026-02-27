using ExpenseTracker.Api.Common;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Wallets;
using ExpenseTracker.Application.Features.Wallets.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController(WalletService walletService) : BaseApiController
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<WalletResponseDto>>), 200)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<WalletResponseDto>>> Get([FromQuery] PaginationRequest request)
    {
        var wallets = await walletService.GetAllByCustomerAsync(request);
        return Ok(wallets);
    }
}