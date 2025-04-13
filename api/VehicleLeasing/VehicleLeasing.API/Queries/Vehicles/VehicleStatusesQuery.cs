using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleStatusesQuery : IRequest<Result<List<VehicleStatusResponse>>>
{
    public class Handler : IRequestHandler<VehicleStatusesQuery, Result<List<VehicleStatusResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<VehicleStatusResponse>>> Handle(VehicleStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _context.VehicleStatuses
                .OrderBy(s => s.Status)
                .Select(s => new VehicleStatusResponse(s.Id, s.Status))
                .ToListAsync(cancellationToken);
        }
    }
}