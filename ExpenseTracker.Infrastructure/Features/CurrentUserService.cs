using System.IdentityModel.Tokens.Jwt;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Infrastructure.Features;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        
        Console.WriteLine("Authenticated: " + httpContext?.User?.Identity?.IsAuthenticated);

        if (httpContext?.User?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException();

        var claim = httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub);

        
        Console.WriteLine("claim: " + claim);

        
        if (claim is null || !Guid.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException();

        return userId;
    }
}