using Domain.Entities;

namespace API.DTOs;

public class HoldingDto
{
    public string Coin { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal BuyPrice { get; set; }

    public decimal TotalPaid { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal WinLoss { get; set; }

    public decimal WinLossPercentage { get; set; }

    public static HoldingDto FromDomain(Holding h) => new()
    {
        Coin = h.CoinSymbol,
        Amount = h.Amount,
        BuyPrice = h.BuyPrice,
        TotalPaid = h.TotalPaid,
        CurrentPrice = h.CurrentPrice,
        CurrentValue = h.CurrentValue,
        WinLoss = h.WinLoss,
        WinLossPercentage = h.WinLossPercentage
    };
}
