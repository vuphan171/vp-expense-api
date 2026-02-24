
using ExpenseTracker.Domain.Common;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Domain.Features.Customers;

public class Customer : BaseEntity
{
    
    public required string FirstName { get; init; }
    
    public required string LastName  { get; init; }
    
    public required string Email {get; init;}
    
    public required DateTime DateOfBirth { get; init; }
    
    [JsonIgnore]
    public string? PasswordHash { get; set; }
    
}