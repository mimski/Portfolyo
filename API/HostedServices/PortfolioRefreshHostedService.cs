using Application.Services;
using Microsoft.Extensions.Options;
using Persistence.Configuration;

namespace API.HostedServices;

public class PortfolioRefreshHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PortfolioRefreshHostedService> _logger;
    private readonly TimeSpan _refreshInterval;

    public PortfolioRefreshHostedService(
        IServiceProvider serviceProvider,
        ILogger<PortfolioRefreshHostedService> logger,
        IOptions<PortfolioRefreshOptions> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        int minutes = options.Value.IntervalMinutes;
        _refreshInterval = TimeSpan.FromMinutes(minutes);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Portfolio auto-refresh scheduled every {Minutes} minutes", _refreshInterval.TotalMinutes);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Scheduled refresh starting...");

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var refreshService = scope.ServiceProvider.GetRequiredService<IRefreshPortfolioService>();

                await refreshService.RefreshPricesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while refreshing portfolio");
            }

            await Task.Delay(_refreshInterval, stoppingToken);
        }
    }
}
