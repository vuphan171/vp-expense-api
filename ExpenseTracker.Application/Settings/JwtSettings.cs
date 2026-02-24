namespace ExpenseTracker.Application.Settings;

public class JwtSettings
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpiresMinutes { get; set; }
    public required string SecretKey { get; set; }
}