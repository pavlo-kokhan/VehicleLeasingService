namespace VehicleLeasing.API.Contracts.QueryParameters.Common;

public record PageParameters(
    int PageSize = 10, 
    int PageNumber = 1);
