namespace VehicleLeasing.API.Contracts.QueryParameters.Common;

public record SortParameters(
    string? OrderBy, 
    bool? Ascending);
