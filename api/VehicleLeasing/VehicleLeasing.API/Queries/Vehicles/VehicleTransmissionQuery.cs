using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleTransmissionQuery(int Id) : IRequest<Result<VehicleTransmissionResponse>>
{
    public class Handler : IRequestHandler<VehicleTransmissionQuery, Result<VehicleTransmissionResponse>>
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
    }
}