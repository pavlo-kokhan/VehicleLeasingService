using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands.LeasingRequests;

public record DeclineLeasingRequestCommand(int Id) : IRequest<Result>
{
    public class Handler : IRequestHandler<DeclineLeasingRequestCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result> Handle(DeclineLeasingRequestCommand request, CancellationToken cancellationToken)
        {
            var newLeasingRequestStatus = await _context.LeasingRequestStatuses
                .FirstOrDefaultAsync(s => s.Status == LeasingRequestStatuses.Declined, cancellationToken);

            if (newLeasingRequestStatus is null)
                return LeasingRequestsValidationErrors.LeasingRequestStatusNotFound;
            
            var leasingRequest = await _context.LeasingRequests
                .Where(r => r.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (leasingRequest is null)
                return LeasingRequestsValidationErrors.LeasingRequestNotFound;
            
            leasingRequest.StatusId = newLeasingRequestStatus.Id;
            
            var newVehicleStatus = await _context.VehicleStatuses
                .FirstOrDefaultAsync(s => s.Status == VehicleStatuses.Available, cancellationToken);

            if (newVehicleStatus is null)
                return VehiclesValidationErrors.VehicleStatusNotFound;
            
            var vehicleUpdateResult = await _context.Vehicles
                .Where(v => v.Id == leasingRequest.VehicleId)
                .ExecuteUpdateAsync(u => u
                        .SetProperty(v => v.StatusId, newVehicleStatus.Id), 
                    cancellationToken);

            if (vehicleUpdateResult < 1)
                return VehiclesValidationErrors.VehicleNotFound;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}