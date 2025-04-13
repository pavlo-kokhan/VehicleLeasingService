using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleFuelTypesQuery : IRequest<Result<List<VehicleFuelTypeResponse>>>
{
    public class Handler : IRequestHandler<VehicleFuelTypesQuery, Result<List<VehicleFuelTypeResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<List<VehicleFuelTypeResponse>>> Handle(VehicleFuelTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.VehicleFuelTypes
                .OrderBy(f => f.Type)
                .Select(f => new VehicleFuelTypeResponse(f.Id, f.Type))
                .ToListAsync(cancellationToken);
        }
    }
}