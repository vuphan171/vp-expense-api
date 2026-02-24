using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Wallets.Dtos;
using ExpenseTracker.Domain.Features.Wallets;

namespace ExpenseTracker.Application.Features.Wallets;

public class WalletService(IWalletRepository repository)
{
    public async Task<PagedResult<WalletResponseDto>> GetAllByCustomerAsync(Guid customerId, PaginationRequest request)
    {
        var (items, totalCount) = await repository.GetAllByCustomerAsync(customerId, request.Page, request.PageSize);

        var wallets = items.Select(w => new WalletResponseDto
        {
            Id = w.Id,

            Balance = w.Balance,
            Currency = w.Currency,
            IsActive = w.IsActive,
            WalletName = w.WalletName,
            CreatedAt = w.CreatedAt,
            UpdatedAt = w.UpdatedAt,
        }).ToList();

        return new PagedResult<WalletResponseDto>
        {
            Items = wallets,
            TotalCount = totalCount,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }
}