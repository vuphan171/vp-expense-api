using ExpenseTracker.Api.Common;
using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Customers;
using ExpenseTracker.Application.Features.Customers.Dtos;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Domain.Features.Customers;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(CustomerService customerService) : BaseApiController
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<CustomerResponseDto>>), 200)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<CustomerResponseDto>>> Get([FromQuery] PaginationRequest request)
    {
        var customers = await customerService.GetAllAsync(request);
        return Ok(customers);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Customer>> Post([FromBody] CreateCustomerDto request)
    {
        var result = await customerService.CreateAsync(request);
        return Ok(result);
    }
}