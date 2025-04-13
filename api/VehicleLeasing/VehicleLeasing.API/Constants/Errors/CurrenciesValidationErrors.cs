using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class CurrenciesValidationErrors
{
    public static readonly ValidationError CurrencyNotFound = ValidationError.CreateWithMessage("CURRENCY_NOT_FOUND", "Currency is not found");
}