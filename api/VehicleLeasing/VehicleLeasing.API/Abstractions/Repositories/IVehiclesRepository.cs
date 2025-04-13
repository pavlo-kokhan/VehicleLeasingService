using VehicleLeasing.API.Contracts.Common;
using VehicleLeasing.API.Contracts.QueryParameters;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.Vehicles;
using VehicleLeasing.API.Contracts.Vehicles;

namespace VehicleLeasing.API.Abstractions.Repositories;

public interface IVehiclesRepository
{
    Task<PagedResponse<VehicleForLeasingResponse>> GetPage(
        VehiclesFilter? filter,
        SortParameters? sortParameters,
        PageParameters? pageParameters);
}