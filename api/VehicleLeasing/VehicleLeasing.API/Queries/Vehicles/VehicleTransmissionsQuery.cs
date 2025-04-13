using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleTransmissionsQuery : IRequest<Result<List<VehicleTransmissionResponse>>>
{
    public class Handler : IRequestHandler<VehicleTransmissionsQuery, Result<List<VehicleTransmissionResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<VehicleTransmissionResponse>>> Handle(VehicleTransmissionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.VehicleTransmissions
                .OrderBy(t => t.Transmission)
                .Select(t => new VehicleTransmissionResponse(t.Id, t.Transmission))
                .ToListAsync(cancellationToken);
        }
    }
}