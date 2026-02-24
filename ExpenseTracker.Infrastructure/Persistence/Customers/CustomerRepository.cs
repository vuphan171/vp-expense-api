using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Features.Customers;
using Dapper;


namespace ExpenseTracker.Infrastructure.Persistence.Customers;

public class CustomerRepository(IUnitOfWork unitOfWork) : ICustomerRepository
{

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var builder = new SqlBuilder();
        
        var sql = builder.AddTemplate(@"Select /**select**/ From customer WHERE email = @Email;");

        builder.Select(@"id, first_name, last_name, email, date_of_birth, created_at, updated_at, password_hash");
        
        builder.AddParameters(new
        {
            Email = email
        });
        
       var customer =  await unitOfWork.Connection.QuerySingleOrDefaultAsync<Customer>(sql.RawSql, sql.Parameters);

       return customer;

    }
    

    public async Task<Customer> AddAsync(Customer customer)
    {
        var builder = new SqlBuilder();

        var template = builder.AddTemplate(@"
        INSERT INTO customer (
            first_name,
            last_name,
            email,
            date_of_birth,
            password_hash
        )
        VALUES (
            @FirstName,
            @LastName,
            @Email,
            @DateOfBirth,
            @PasswordHash
        );
    ");

        builder.AddParameters(customer);

        await unitOfWork.Connection.ExecuteAsync(
            template.RawSql,
            template.Parameters
        );

        return customer;
    }

    public async Task<(List<Customer> Items, int TotalCount)> GetCustomersAsync(int pageNumber, int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;

        var builder = new SqlBuilder();

        var sql = builder.AddTemplate(@"
        SELECT /**select**/
        FROM customer
        ORDER BY created_at DESC
        LIMIT @PageSize OFFSET @Offset;

        SELECT COUNT(*) FROM customer;
    ");


        builder.Select(@"id, first_name, last_name, email, date_of_birth, created_at, updated_at");


        builder.AddParameters(new
        {
            Offset = offset,
            PageSize = pageSize
        });

        var multi = await unitOfWork.Connection.QueryMultipleAsync(
            sql.RawSql,
            sql.Parameters
        );

        var items = (await multi.ReadAsync<Customer>()).ToList();
        var total = await multi.ReadSingleAsync<int>();

        return (Items: items, TotalCount: total);
    }
}