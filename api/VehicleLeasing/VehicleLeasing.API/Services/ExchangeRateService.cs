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
    
    public async Task<List<ExchangeRateDto>> GetRatesToUsdAsync(DateOnly targetDate)
    {
        var usdResponse = await GetResponseDtosAsync(targetDate, CurrencyCodes.Usd);
        var eurResponse = await GetResponseDtosAsync(targetDate, CurrencyCodes.Eur);

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
    
    public async Task<List<ExchangeRateDto>> GetRatesTableToUahAsync(DateOnly targetDate)
    {
        var result = new List<ExchangeRateDto>();
        
        var currencyCodes = new[] { CurrencyCodes.Usd, CurrencyCodes.Eur, CurrencyCodes.Pln, CurrencyCodes.Gbp };

        foreach (var code in currencyCodes)
        {
            var rates = await GetResponseDtosAsync(targetDate, code);
            
            if (rates is not null && rates.Any())
            {
                result.Add(rates.First());
            }
        }

        return result;
    }

    private async Task<ExchangeRateDto[]?> GetResponseDtosAsync(DateOnly targetDate, string currencyCode)
    {
        var date = targetDate.ToString("yyyyMMdd");
        
        return await _httpClient.GetFromJsonAsync<ExchangeRateDto[]>(
            $"{_options.Value.NbuBaseUrl}?start={date}&end={date}&valcode={currencyCode}&sort=exchangedate&order=desc&json");
    }
}