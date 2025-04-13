using System.Text.Json.Serialization;

namespace VehicleLeasing.API.Contracts.ExchangeRates;

public class ExchangeRateDto
{
    [JsonPropertyName("cc")]
    public string CurrencyCode { get; set; } = null!;

    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }
}