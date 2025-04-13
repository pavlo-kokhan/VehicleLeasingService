using VehicleLeasing.API.Results;

namespace VehicleLeasing.API.Contracts.Responses;

public record ErrorResponse(IDictionary<string, string?> Errors, ResultStatus ResultStatus);