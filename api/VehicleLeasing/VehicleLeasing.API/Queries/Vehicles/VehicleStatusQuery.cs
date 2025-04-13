using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleStatusQuery(int Id) : IRequest<Result<VehicleStatusResponse>>
{
    public class Handler : IRequestHandler<VehicleStatusQuery, Result<VehicleStatusResponse>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<VehicleTransmissionResponse>> Handle(VehicleTransmissionQuery request, CancellationToken cancellationToken)
        {
            var transmission = await _context.VehicleTransmissions
                .Where(t => t.Id == request.Id)
                .Select(t => new VehicleTransmissionResponse(t.Id, t.Transmission))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (transmission is null) return VehiclesValidationErrors.VehicleTransmissionNotFound;
            
            return transmission;
        }

        public async Task<Result<VehicleStatusResponse>> Handle(VehicleStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _context.VehicleStatuses
                .Where(s => s.Id == request.Id)
                .Select(s => new VehicleStatusResponse(s.Id, s.Status))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (status is null) return VehiclesValidationErrors.VehicleStatusNotFound;
            
            return status;
        }
    }
}