using Dapper;
using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Features.Wallets;

namespace ExpenseTracker.Infrastructure.Persistence.Wallets;

public class WalletRepository(IUnitOfWork unitOfWork) : IWalletRepository
{
    public Task<Wallet> AddAsync(Wallet entity)
    {
        throw new NotImplementedException();
    }

    public Task<(List<Wallet> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }


    public async Task<(List<Wallet> Items, int TotalCount)> GetAllByCustomerAsync(Guid customerId, int pageNumber, int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;


        var builder = new SqlBuilder();


        var sql = builder.AddTemplate(@"
            SELECT /**select**/
            FROM public.wallet
            ORDER BY created_at DESC
            LIMIT @PageSize OFFSET @Offset;

            SELECT COUNT(*) FROM wallet;
        ");

        builder.Select(@"id, wallet_name, customer_id, balance, currency, is_active, created_at, updated_at");

        builder.Where("customer_id = @CustomerId", new { CustomerId = customerId });

        builder.AddParameters(new
        {
            Offset = offset,
            PageSize = pageSize
        });

        var multi = await unitOfWork.Connection.QueryMultipleAsync(
            sql.RawSql,
            sql.Parameters
        );

        var items = (await multi.ReadAsync<Wallet>()).ToList();
        var total = await multi.ReadSingleAsync<int>();

        return (Items: items, TotalCount: total);
    }

    public Task<Wallet> UpdateAsync(Wallet entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Wallet entity)
    {
        throw new NotImplementedException();
    }

   
}