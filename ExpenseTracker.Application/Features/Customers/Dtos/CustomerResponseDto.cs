using ExpenseTracker.Domain.Common;

namespace ExpenseTracker.Application.Features.Customers.Dtos;

public class CustomerResponseDto: BaseEntity
{
    public required string FirstName { get; init; }
    
    public required string LastName  { get; init; }
    
    public required string Email {get; init;}
    
    public required DateTime DateOfBirth { get; init; }
}