using ExpenseTracker.Api.Common;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Categories;
using ExpenseTracker.Application.Features.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(CategoryService categoryService): BaseApiController
{

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<CategoryResponseDto>>> Get([FromQuery] PaginationRequest request)
    {
        var categories = await categoryService.GetAllAsync(request);
        return Ok(categories);
    }
}