namespace VehicleLeasing.API.Contracts.QueryParameters.Common;

public record PageParameters(
    int PageSize = 20, 
    int PageNumber = 1);
