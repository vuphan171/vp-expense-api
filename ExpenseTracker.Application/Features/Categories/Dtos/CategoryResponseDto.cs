using ExpenseTracker.Domain.Common;
using ExpenseTracker.Domain.Features.Categories;

namespace ExpenseTracker.Application.Features.Categories.Dtos;

public class CategoryResponseDto: BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required CategoryType Type { get; set; }
}