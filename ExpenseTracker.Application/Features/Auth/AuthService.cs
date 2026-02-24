using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Application.Features.Auth.Dtos;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Features.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Application.Features.Auth;

public class AuthService(ICustomerRepository customerRepository, IPasswordHasher<Customer> passwordHasher, IJwtTokenService jwtTokenService)
{
    public async Task<string> Login(LoginRequestDto loginRequestDto)
    {
        var customer = await customerRepository.GetCustomerByEmailAsync(loginRequestDto.Email);

        if (customer?.PasswordHash is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }


        var result = passwordHasher.VerifyHashedPassword(
            customer,
            customer.PasswordHash,
            loginRequestDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var token = jwtTokenService.GenerateToken(customer);
        return token;
    }
}