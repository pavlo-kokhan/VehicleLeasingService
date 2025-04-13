﻿namespace VehicleLeasing.API.Contracts.Vehicles;

public record VehicleResponse(
    int Id,
    string Brand,
    string Model,
    int Year,
    decimal EstimatedPrice,
    string Category,
    string Transmission,
    string FuelType,
    string Status,
    DateTime? LastModified);