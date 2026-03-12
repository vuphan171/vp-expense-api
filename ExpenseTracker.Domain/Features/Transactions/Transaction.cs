using ExpenseTracker.Domain.Common;

namespace ExpenseTracker.Domain.Features.Transactions;

public class Transaction : BaseEntity
{
    public required Guid CustomerId { get; set; }
    public required Guid WalletId { get; set; }
    public required decimal  Amount { get; set; }
}