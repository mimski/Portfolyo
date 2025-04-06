using Domain.Entities;

namespace Application.Services;

public interface IUploadPortfolioService
{
    Task<Portfolio> UploadAndComputeAsync(Stream fileStream);
}
