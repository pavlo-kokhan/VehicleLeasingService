using VehicleLeasing.API.Contracts.Users;
using VehicleLeasing.API.Contracts.Vehicles;

namespace VehicleLeasing.API.Contracts.LeasingRequests;

public record LeasingRequestResponse(
    int Id,
    VehicleResponse Vehicle,
    UserResponse User,
    decimal FixedPrice,
    DateOnly Date,
    string Status,
    DateTime? LastModified);