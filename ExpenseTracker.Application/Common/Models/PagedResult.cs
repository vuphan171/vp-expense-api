namespace ExpenseTracker.Application.Common.Models;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; init; }
    public int PageNumber { get; set; }
    public int PageSize { get; init; }

    public int TotalPages =>
        (int)Math.Ceiling((double)TotalCount / PageSize);
}