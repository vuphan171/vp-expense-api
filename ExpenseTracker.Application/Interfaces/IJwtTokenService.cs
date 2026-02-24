using ExpenseTracker.Domain.Features.Customers;

namespace ExpenseTracker.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Customer customer);
    bool ValidateToken(string token);
}