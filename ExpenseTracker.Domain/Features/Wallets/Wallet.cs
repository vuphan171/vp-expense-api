using ExpenseTracker.Domain.Common;

namespace ExpenseTracker.Domain.Features.Wallets;

public enum Currency
{
    Usd,
    Eur,
    Vnd
}

public class Wallet: BaseEntity
{
    public required Guid CustomerId { get; set; }
    public required string WalletName { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency  { get; set; }
    public required bool IsActive { get; set; }
}