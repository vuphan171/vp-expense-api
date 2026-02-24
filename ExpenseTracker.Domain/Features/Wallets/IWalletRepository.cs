using ExpenseTracker.Domain.Common;

namespace ExpenseTracker.Domain.Features.Wallets;

public interface IWalletRepository : IRepository<Wallet>
{
    Task<(List<Wallet> Items, int TotalCount)> GetAllByCustomerAsync(Guid customerId, int pageNumber, int pageSize);
}