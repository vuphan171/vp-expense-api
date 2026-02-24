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
        var mockGuid = Guid.Parse("09fe5a14-0b6e-45e5-bac9-52c905af6440");
        var wallets = await walletService.GetAllByCustomerAsync(mockGuid,request);
        return Ok(wallets);
    }

   
}