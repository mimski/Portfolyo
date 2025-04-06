namespace Domain.Entities;

public class Holding
{
    public decimal Amount { get; set; }

    public string CoinSymbol { get; set; } = string.Empty;

    public decimal BuyPrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal TotalPaid { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal WinLoss { get; set; }

    public decimal WinLossPercentage { get; set; }

    public bool IsValid()
    {
        return Amount > 0 &&
               BuyPrice >= 0 &&
               !string.IsNullOrWhiteSpace(CoinSymbol);
    }
}
