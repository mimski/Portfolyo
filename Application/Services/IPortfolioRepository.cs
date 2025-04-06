using Domain.Entities;

namespace Application.Services;

public interface IPortfolioRepository
{
    void SavePortfolio(Portfolio portfolio);

    Portfolio? GetPortfolio();
}
