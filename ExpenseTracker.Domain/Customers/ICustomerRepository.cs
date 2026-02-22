namespace ExpenseTracker.Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);

    Task<(List<Customer> Items, int TotalCount)> GetCustomersAsync(int pageNumber, int pageSize);
}