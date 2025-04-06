namespace Persistence.Models;

public class CoinloreResponse
{
    public List<CoinloreCoin> Data { get; set; } = new();
}

public class CoinloreCoin
{
    public string symbol { get; set; } = string.Empty;

    public string price_usd { get; set; } = string.Empty;
}
