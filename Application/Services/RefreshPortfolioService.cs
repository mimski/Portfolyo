using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// Scheduled task that refetches coin prices and updates the existing portfolio
/// </summary>
public class RefreshPortfolioService : IRefreshPortfolioService
{
    private readonly ICoinPriceService coinPriceService;
    private readonly IPortfolioRepository portfolioRepo;
    private readonly ILogger<RefreshPortfolioService> logger;

    public RefreshPortfolioService(
        ICoinPriceService coinPriceService,
        IPortfolioRepository portfolioRepo,
        ILogger<RefreshPortfolioService> logger)
    {
        this.coinPriceService = coinPriceService;
        this.portfolioRepo = portfolioRepo;
        this.logger = logger;
    }

    public async Task<Portfolio?> RefreshPricesAsync()
    {
        var existing = portfolioRepo.GetPortfolio();

        if (existing == null)
        {
            logger.LogInformation("No portfolio found to refresh.");

            return null;
        }

        logger.LogInformation("Refreshing existing portfolio with {Count} holdings", existing.Holdings.Count);

        var symbols = existing.Holdings.Select(h => h.CoinSymbol).Distinct().ToList();
        var currentPrices = await coinPriceService.GetPricesAsync(symbols);

        foreach (var h in existing.Holdings)
        {
            decimal price = currentPrices.TryGetValue(h.CoinSymbol, out decimal value)
                ? value : 0m;

            h.CurrentPrice = price;
            h.CurrentValue = h.Amount * h.CurrentPrice;
            h.WinLoss = h.CurrentValue - h.TotalPaid;
            h.WinLossPercentage = h.TotalPaid != 0
                ? (h.WinLoss / h.TotalPaid) * 100
                : 0;
        }

        existing.LastUpdated = DateTime.UtcNow;

        portfolioRepo.SavePortfolio(existing);

        logger.LogInformation("Portfolio refresh complete.");

        return existing;
    }
}
