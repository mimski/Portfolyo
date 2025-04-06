using Domain.Entities;

namespace Application.Services;

public interface IGetPortfolioService
{
    Portfolio? GetCurrentPortfolio();
}
