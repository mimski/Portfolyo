using Application.Services;
using Domain.Entities;

namespace Persistence.Repositories;

public class InMemoryPortfolioRepository : IPortfolioRepository
{
    private Portfolio? _portfolioCache;
    private readonly object _lock = new();

    public void SavePortfolio(Portfolio portfolio)
    {
        lock (_lock)
        {
            _portfolioCache = portfolio;
        }
    }

    public Portfolio? GetPortfolio()
    {
        lock (_lock)
        {
            // returns the reference
            return _portfolioCache;
        }
    }
}
