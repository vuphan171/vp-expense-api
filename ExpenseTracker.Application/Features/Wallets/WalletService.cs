using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Features.Wallets.Dtos;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Features.Wallets;

namespace ExpenseTracker.Application.Features.Wallets;

public class WalletService(IWalletRepository repository, ICurrentUserService currentUserService)
{
    public async Task<PagedResult<WalletResponseDto>> GetAllByCustomerAsync( PaginationRequest request)
    {
        var userId = currentUserService.GetUserId();
        
        var (items, totalCount) = await repository.GetAllByCustomerAsync(userId, request.Page, request.PageSize);

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

    public async Task<Wallet> CreateAsync(CreateWalletDto createWalletDto)
    {
        var customerId = currentUserService.GetUserId();
        var wallet = new Wallet
        {
            CustomerId = customerId,
            WalletName = createWalletDto.WalletName,
            Balance = createWalletDto.Balance,
            Currency = createWalletDto.Currency,
            IsActive = true
        };
        
        var result = await repository.AddAsync(wallet);
        
        return result;
    }
}