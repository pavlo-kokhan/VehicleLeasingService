namespace VehicleLeasing.API.Contracts.Users;

public record UserResponse(
    Guid Id,
    string Role,
    string Name,
    string Surname,
    string Email,
    string PasswordHash);
