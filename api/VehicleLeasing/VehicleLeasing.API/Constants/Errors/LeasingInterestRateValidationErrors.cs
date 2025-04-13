using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class LeasingInterestRateValidationErrors
{
    public static readonly ValidationError InterestRateNotFound = ValidationError.CreateWithMessage("INTEREST_RATE_NOT_FOUND", "Interest rate is not found");
}