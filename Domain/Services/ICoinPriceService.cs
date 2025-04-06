namespace Domain.Services;

public interface ICoinPriceService
{
    Task<Dictionary<string, decimal>> GetPricesAsync(IEnumerable<string> symbols);
}
