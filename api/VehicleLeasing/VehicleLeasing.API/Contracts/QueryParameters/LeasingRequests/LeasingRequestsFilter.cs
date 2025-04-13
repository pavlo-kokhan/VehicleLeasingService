namespace VehicleLeasing.API.Contracts.QueryParameters.LeasingRequests;

public record LeasingRequestsFilter(
    string? VehicleBrand,
    string? VehicleModel,
    string? UserName,
    DateOnly? MinDate,
    DateOnly? MaxDate,
    string? Status,
    DateTime? MinLastModified,
    DateTime? MaxLastModified);