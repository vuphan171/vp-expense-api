using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Customers.Dtos;
using ExpenseTracker.Domain.Features.Customers;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Application.Features.Customers;

public class CustomerService(ICustomerRepository repository, IPasswordHasher<Customer> passwordHasher)
{
    public async Task<PagedResult<CustomerResponseDto>> GetAllAsync(
        PaginationRequest request)
    {
        var (items, totalCount) = await repository.GetCustomersAsync(request.Page, request.PageSize);

        var customers = items.Select(c => new CustomerResponseDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            DateOfBirth = c.DateOfBirth,
            Email = c.Email,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
        }).ToList();

        return new PagedResult<CustomerResponseDto>
        {
            Items = customers,
            TotalCount = totalCount,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task<Customer> CreateAsync(CreateCustomerDto customerDto)
    {
        var customer = new Customer
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            DateOfBirth = customerDto.DateOfBirth,
        };

        customer.PasswordHash =
            passwordHasher.HashPassword(customer, customerDto.Password);

        var result = await repository.AddAsync(customer);

        return result;
    }
}