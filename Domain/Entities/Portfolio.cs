namespace Domain.Entities;

public class Portfolio
{
    public List<Holding> Holdings { get; set; } = new();

    public decimal TotalPaid => Holdings.Sum(h => h.TotalPaid);

    public decimal TotalValue => Holdings.Sum(h => h.CurrentValue);

    public decimal TotalWinLoss => TotalValue - TotalPaid;

    public decimal TotalWinLossPercentage => (TotalPaid != 0) ? (TotalWinLoss / TotalPaid) * 100 : 0;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
