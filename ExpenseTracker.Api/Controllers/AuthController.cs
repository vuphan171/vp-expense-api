using ExpenseTracker.Application.Features.Auth;
using ExpenseTracker.Application.Features.Auth.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : BaseApiController
{
    
    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] LoginRequestDto request)
    {
        var result = await authService.Login(request);
        return Ok(result);
    }
}