using Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Configuration;
using Persistence.Models;
using System.Net.Http.Json;
using System.Runtime.Caching;

namespace Persistence.Services;

public class CoinloreService : ICoinPriceService
{
    private readonly HttpClient _client;
    private readonly MemoryCache _cache;
    private readonly ILogger<CoinloreService> _logger;
    private readonly CoinPriceOptions _options;

    public CoinloreService(HttpClient client, ILogger<CoinloreService> logger, IOptions<CoinPriceOptions> options)
    {
        _client = client;
        _logger = logger;
        _cache = MemoryCache.Default;
        _options = options.Value;
    }

    public async Task<Dictionary<string, decimal>> GetPricesAsync(IEnumerable<string> symbols)
    {
        var result = new Dictionary<string, decimal>();
        var missingSymbols = new List<string>();

        // Check what in cache
        foreach (var symbol in symbols)
        {
            string cacheKey = $"coinprice-{symbol.ToUpper()}";
            if (_cache.Contains(cacheKey))
            {
                result[symbol] = (decimal)_cache.Get(cacheKey)!;
            }
            else
            {
                missingSymbols.Add(symbol);
            }
        }

        if (missingSymbols.Count > 0)
        {
            _logger.LogInformation("Fetching {Count} symbols from Coinlore", missingSymbols.Count);

            var response = await _client.GetFromJsonAsync<CoinloreResponse>("api/tickers/?start=0&limit=100");

            if (response == null || response.Data == null)
            {
                throw new Exception("Coinlore response is empty or invalid");
            }

            foreach (var symbol in missingSymbols)
            {
                var coin = response.Data
                    .FirstOrDefault(c => c.symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
                if (coin == null || !decimal.TryParse(coin.price_usd, out decimal priceUsd))
                {
                    _logger.LogWarning("Coin {Symbol} not found or parse error. Defaulting price to 0", symbol);
                    result[symbol] = 0;
                }
                else
                {
                    result[symbol] = priceUsd;

                    // store in cache
                    string cacheKey = $"coinprice-{symbol.ToUpper()}";
                    _cache.Set(cacheKey, priceUsd, DateTimeOffset.Now.AddMinutes(_options.CacheMinutes));
                }
            }
        }

        return result;
    }
}
