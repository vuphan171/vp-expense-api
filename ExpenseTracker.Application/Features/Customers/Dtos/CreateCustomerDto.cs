namespace ExpenseTracker.Application.Features.Customers.Dtos;

public class CreateCustomerDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required DateTime DateOfBirth { get; init; }
    public required string Password { get; init; }
}