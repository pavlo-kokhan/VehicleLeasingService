using VehicleLeasing.API.Contracts.ExchangeRates;

namespace VehicleLeasing.API.Abstractions.Services;

public interface IExchangeRateService
{
    Task<List<ExchangeRateDto>> GetRatesToUsdAsync(DateOnly targetDate);
    Task<List<ExchangeRateDto>> GetRatesTableToUahAsync(DateOnly targetDate);
}