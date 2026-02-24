namespace ExpenseTracker.Domain.Common;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);

    Task<(List<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);

    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}