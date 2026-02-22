using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Customers;
using ExpenseTracker.Application.Features.Customers.Dtos;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Domain.Customers;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(CustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<CustomerResponseDto>>> Get([FromQuery] PaginationRequest request)
    {
        var customers = await customerService.GetAllAsync(request);
        return customers;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Post([FromBody] CreateCustomerDto request)
    {
        var result = await customerService.CreateAsync(request);
        return Ok(result);
    }
}