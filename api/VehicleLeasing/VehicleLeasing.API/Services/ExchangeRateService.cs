using Microsoft.Extensions.Options;
using VehicleLeasing.API.Abstractions.Services;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Contracts.ExchangeRates;

namespace VehicleLeasing.API.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<ExchangeRatesOptions> _options;

    public ExchangeRateService(HttpClient httpClient, IOptions<ExchangeRatesOptions> options)
    {
        _httpClient = httpClient;
        _options = options;
    }
    
    public async Task<List<ExchangeRateDto>> GetRatesAsync(DateOnly targetDate)
    {
        var date = targetDate.ToString("yyyyMMdd");
    
        var usdResponse = await _httpClient.GetFromJsonAsync<ExchangeRateDto[]>(
            $"{_options.Value.NbuBaseUrl}?start={date}&end={date}&valcode={CurrencyCodes.Usd}&sort=exchangedate&order=desc&json");
    
        var eurResponse = await _httpClient.GetFromJsonAsync<ExchangeRateDto[]>(
            $"{_options.Value.NbuBaseUrl}?start={date}&end={date}&valcode={CurrencyCodes.Eur}&sort=exchangedate&order=desc&json");

        if (usdResponse is null || eurResponse is null || !usdResponse.Any() || !eurResponse.Any())
            return new();

        var usdToUah = usdResponse.First().Rate;
        var eurToUah = eurResponse.First().Rate;

        var usdToEur = Math.Round(usdToUah / eurToUah, 4);

        return new()
        {
            new()
            {
                CurrencyCode = CurrencyCodes.Uah,
                Rate = usdToUah
            },
            new()
            {
                CurrencyCode = CurrencyCodes.Eur,
                Rate = usdToEur
            }
        };
    }
}