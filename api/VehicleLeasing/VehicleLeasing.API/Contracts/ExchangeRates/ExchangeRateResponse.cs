namespace VehicleLeasing.API.Contracts.ExchangeRates;

public record ExchangeRateResponse(
    int Id,
    string CurrencyCode,
    decimal Rate,
    DateOnly Date);