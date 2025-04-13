using VehicleLeasing.API.Contracts.Users;

namespace VehicleLeasing.API.Abstractions.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserResponse userResponse);
}