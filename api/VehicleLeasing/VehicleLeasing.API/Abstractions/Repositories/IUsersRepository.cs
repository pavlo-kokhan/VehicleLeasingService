using VehicleLeasing.API.Contracts.Users;

namespace VehicleLeasing.API.Abstractions.Repositories;

public interface IUsersRepository
{
    Task<Guid> Add(UserResponse userResponse);
    Task<UserResponse?> GetByEmail(string email);
}