using ExpenseTracker.Domain.Features.Wallets;

namespace ExpenseTracker.Application.Features.Wallets.Dtos;

public class CreateWalletDto
{
    public required string WalletName { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
}