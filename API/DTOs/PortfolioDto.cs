using Domain.Entities;

namespace API.DTOs;

public class PortfolioDto
{
    public decimal InitialValue { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal TotalWinLoss { get; set; }

    public decimal TotalWinLossPercentage { get; set; }

    public DateTime LastUpdated { get; set; }

    public List<HoldingDto> Holdings { get; set; } = new();

    public static PortfolioDto FromDomain(Portfolio portfolio)
    {
        return new PortfolioDto
        {
            InitialValue = portfolio.TotalPaid,
            CurrentValue = portfolio.TotalValue,
            TotalWinLoss = portfolio.TotalWinLoss,
            TotalWinLossPercentage = portfolio.TotalWinLossPercentage,
            LastUpdated = portfolio.LastUpdated,
            Holdings = portfolio.Holdings.Select(HoldingDto.FromDomain).ToList()
        };
    }
}
