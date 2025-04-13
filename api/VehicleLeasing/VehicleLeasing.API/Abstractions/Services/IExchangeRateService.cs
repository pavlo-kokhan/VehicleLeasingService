using VehicleLeasing.API.Contracts.ExchangeRates;

namespace VehicleLeasing.API.Abstractions.Services;

public interface IExchangeRateService
{
    Task<List<ExchangeRateDto>> GetRatesAsync(DateOnly targetDate);
}