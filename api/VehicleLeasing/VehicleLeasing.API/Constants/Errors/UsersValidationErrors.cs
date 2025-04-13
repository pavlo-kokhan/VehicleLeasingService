using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class UsersValidationErrors
{
    public static readonly ValidationError UserNotFound = ValidationError.CreateWithMessage("USER_NOT_FOUND", "User is not found");
}