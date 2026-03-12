using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Categories.Dtos;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Features.Categories;

namespace ExpenseTracker.Application.Features.Categories;

public class CategoryService(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
{
    public async Task<PagedResult<CategoryResponseDto>> GetAllAsync(
        PaginationRequest request)
    {
        var customerId = currentUserService.GetUserId();
        var (items, totalCount) =
            await categoryRepository.GetAllByCustomerAsync(customerId, request.Page, request.PageSize);

        var categories = items.Select(w => new CategoryResponseDto
        {
            Id = w.Id,
            Name = w.Name,
            Type = w.Type,
            Description = w.Description,

            CreatedAt = w.CreatedAt,
            UpdatedAt = w.UpdatedAt,
        }).ToList();

        return new PagedResult<CategoryResponseDto>
        {
            Items = categories,
            TotalCount = totalCount,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }
}