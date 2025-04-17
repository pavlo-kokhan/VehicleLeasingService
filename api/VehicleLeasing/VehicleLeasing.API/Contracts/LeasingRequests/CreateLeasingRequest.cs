namespace VehicleLeasing.API.Contracts.LeasingRequests;

public record CreateLeasingRequest(
    int VehicleId,
    decimal FixedPrice);