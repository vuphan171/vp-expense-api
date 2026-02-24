using ExpenseTracker.Application.Features.Auth;
using ExpenseTracker.Application.Features.Auth.Dtos;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Domain.Features.Customers;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<Customer>> Post([FromBody] LoginRequestDto request)
    {
        var result = await authService.Login(request);
        return Ok(result);
    }
}