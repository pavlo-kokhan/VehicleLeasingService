namespace VehicleLeasing.API.Contracts.QueryParameters.Vehicles;

public record VehiclesFilter(
    string? Brand,
    string? Model,
    int? MinYear,
    int? MaxYear,
    decimal? MinEstimatedPrice,
    decimal? MaxEstimatedPrice);
