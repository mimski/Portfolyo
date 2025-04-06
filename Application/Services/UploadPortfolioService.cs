using Domain.Entities;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

/// <summary>
/// Handles uploading the user’s holdings file, parsing and computing immediate portfolio info.
/// </summary>
public class UploadPortfolioService : IUploadPortfolioService
{
    private readonly ICoinPriceService coinPriceService;
    private readonly IPortfolioRepository portfolioRepo;
    private readonly ILogger<UploadPortfolioService> logger;

    public UploadPortfolioService(
        ICoinPriceService coinPriceService,
        IPortfolioRepository portfolioRepo,
        ILogger<UploadPortfolioService> logger)
    {
        this.coinPriceService = coinPriceService;
        this.portfolioRepo = portfolioRepo;
        this.logger = logger;
    }

    public async Task<Portfolio> UploadAndComputeAsync(Stream fileStream)
    {
        logger.LogInformation("Starting portfolio upload...");
        var holdings = new List<Holding>();

        using var reader = new StreamReader(fileStream);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Format: "XXX.XXXX|COIN|Y.YYYY"
            var parts = line.Split('|');
            if (parts.Length != 3)
            {
                logger.LogWarning("Skipping invalid line: {line}", line);
                continue;
            }

            if (!decimal.TryParse(parts[0], out var amount) ||
                !decimal.TryParse(parts[2], out var buyPrice))
            {
                logger.LogWarning("Skipping line due to parse error: {line}", line);
                continue;
            }

            string coinSymbol = parts[1].Trim();

            var holding = new Holding
            {
                Amount = amount,
                CoinSymbol = coinSymbol,
                BuyPrice = buyPrice
            };

            if (!holding.IsValid())
            {
                logger.LogWarning("Skipping invalid holding: {Symbol}, Amount: {Amount}, BuyPrice: {Price}", holding.CoinSymbol, holding.Amount, holding.BuyPrice);

                continue;
            }

            holdings.Add(holding);
        }

        var symbols = holdings.Select(h => h.CoinSymbol).Distinct().ToList();

        var currentPrices = await coinPriceService.GetPricesAsync(symbols);

        foreach (var h in holdings)
        {
            decimal price = currentPrices.TryGetValue(h.CoinSymbol, out decimal value)
                ? value : 0m;

            h.CurrentPrice = price;
            h.TotalPaid = h.Amount * h.BuyPrice;
            h.CurrentValue = h.Amount * h.CurrentPrice;
            h.WinLoss = h.CurrentValue - h.TotalPaid;
            h.WinLossPercentage = h.TotalPaid != 0
                ? (h.WinLoss / h.TotalPaid) * 100
                : 0;
        }

        var portfolio = new Portfolio
        {
            Holdings = holdings,
            LastUpdated = DateTime.UtcNow
        };

        portfolioRepo.SavePortfolio(portfolio);

        logger.LogInformation("Portfolio upload complete. {Count} holdings saved.", holdings.Count);

        return portfolio;
    }
}
