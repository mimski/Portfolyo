using Domain.Entities;

namespace Application.Services;

public class GetPortfolioService : IGetPortfolioService
{
    private readonly IPortfolioRepository portfolioRepo;

    public GetPortfolioService(IPortfolioRepository portfolioRepo)
    {
        this.portfolioRepo = portfolioRepo;
    }

    public Portfolio? GetCurrentPortfolio()
    {
        return portfolioRepo.GetPortfolio();
    }
}
