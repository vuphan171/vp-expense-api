using ExpenseTracker.Domain.Common;

namespace ExpenseTracker.Domain.Features.Categories;

public enum CategoryType
{
    Expense,
    Income,
}

public class Category: BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required CategoryType Type { get; set; }
}