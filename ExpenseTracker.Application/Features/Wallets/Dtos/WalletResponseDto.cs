using ExpenseTracker.Domain.Common;
using ExpenseTracker.Domain.Features.Wallets;

namespace ExpenseTracker.Application.Features.Wallets.Dtos;

public class WalletResponseDto:  BaseEntity
{
    public required string WalletName { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency  { get; set; }
    public required bool IsActive { get; set; }
}