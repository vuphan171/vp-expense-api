using System.Text;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Settings;
using ExpenseTracker.Domain.Features.Customers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace ExpenseTracker.Infrastructure.Features;

public class JwtTokenService(IOptions<JwtSettings> jwtSettings) : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(Customer customer)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Sub] = customer.Id.ToString(),
            [JwtRegisteredClaimNames.Email] = customer.Email,
            ["firstName"] = customer.FirstName,
            ["lastName"] = customer.LastName,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString()
        };
        
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = credentials,
            Claims = claims
        };

        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}