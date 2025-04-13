using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleFuelTypeQuery(int Id) : IRequest<Result<VehicleFuelTypeResponse>>
{
    public class Handler : IRequestHandler<VehicleFuelTypeQuery, Result<VehicleFuelTypeResponse>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<VehicleFuelTypeResponse>> Handle(VehicleFuelTypeQuery request, CancellationToken cancellationToken)
        {
            var fuelType = await _context.VehicleFuelTypes
                .Where(f => f.Id == request.Id)
                .Select(f => new VehicleFuelTypeResponse(f.Id, f.Type))
                .FirstOrDefaultAsync(cancellationToken);

            if (fuelType is null) return VehiclesValidationErrors.VehicleFuelTypeNotFound;

            return fuelType;
        }
    }
}