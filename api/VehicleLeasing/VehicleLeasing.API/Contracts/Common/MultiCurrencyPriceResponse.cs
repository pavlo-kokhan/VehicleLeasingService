namespace VehicleLeasing.API.Contracts.Common;

public record MultiCurrencyPriceResponse(
    decimal PriceUsd,
    decimal PriceEur,
    decimal PriceUah);