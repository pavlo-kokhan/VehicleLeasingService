using VehicleLeasing.API.Services;
using VehicleLeasing.DataAccess.DbContexts;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.BackgroundServices;

public class ExchangeRatesUpdaterBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;

    public ExchangeRatesUpdaterBackgroundService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            var nextRunTime = DateTime.Today.AddHours(12);
            
            if (now > nextRunTime)
                nextRunTime = nextRunTime.AddDays(1);
            
            var delay = nextRunTime - now;
            await Task.Delay(delay, stoppingToken);

            using var scope = _services.CreateScope();
            
            var dbContext = scope.ServiceProvider.GetRequiredService<VehicleLeasingDbContext>();
            var exchangeRateService = scope.ServiceProvider.GetRequiredService<ExchangeRateService>();
            
            var today = DateOnly.FromDateTime(DateTime.Today);
            var todayRates = await exchangeRateService.GetRatesToUsdAsync(today);

            if (!todayRates.Any()) return;

            foreach (var rate in todayRates)
            {
                await dbContext.ExchangeRates.AddAsync(new ExchangeRate
                {
                    Rate = rate.Rate,
                    CurrencyCode = rate.CurrencyCode,
                    Date = today
                }, stoppingToken);
            }
            
            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}