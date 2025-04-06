using Domain.Entities;

namespace Application.Services;

public interface IRefreshPortfolioService
{
    Task<Portfolio?> RefreshPricesAsync();
}
