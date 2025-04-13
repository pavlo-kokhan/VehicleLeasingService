using VehicleLeasing.API.Contracts.Validation;

namespace VehicleLeasing.API.Constants.Errors;

public static class AuthValidationErrors
{
    public static readonly ValidationError InvalidEmail = ValidationError.CreateWithMessage("INVALID_EMAIL", "Invalid email");
    public static readonly ValidationError InvalidPassword = ValidationError.CreateWithMessage("INVALID_PASSWORD", "Invalid password");
    public static readonly ValidationError EmailAlreadyExists = ValidationError.CreateWithMessage("EMAIL_EXISTS", "User with this email already exists");
}