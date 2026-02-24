namespace ExpenseTracker.Application.Features.Auth.Dtos;

public class LoginRequestDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}