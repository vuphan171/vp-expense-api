using ExpenseTracker.Api.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ActionResult<T> Ok<T>(T data, string? message = null)
    {
        return base.Ok(ApiResponse<T>.Ok(data, message));
    }

    
    protected IActionResult Fail(string message)
    {
        return BadRequest(ApiResponse<object>.Fail(message));
    }

    protected IActionResult NotFoundResponse(string message)
    {
        return NotFound(ApiResponse<object>.Fail(message));
    }
    
    
}