using VehicleLeasing.API.Contracts.Common;

namespace VehicleLeasing.API.Contracts.Vehicles;

public record VehicleForLeasingResponse(
    int Id,
    string Brand,
    string Model,
    int Year,
    decimal EstimatedPrice,
    MultiCurrencyPriceResponse LeasingPrice,
    string Category,
    string Transmission,
    string FuelType,
    string Status);
