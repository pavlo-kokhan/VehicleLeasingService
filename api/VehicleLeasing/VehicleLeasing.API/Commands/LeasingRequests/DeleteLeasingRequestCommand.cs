using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands.LeasingRequests;

public record DeleteLeasingRequestCommand(int Id) : IRequest<Result>
{
    public class Handler : IRequestHandler<DeleteLeasingRequestCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result> Handle(DeleteLeasingRequestCommand request, CancellationToken cancellationToken)
        {
            var newVehicleStatus = await _context.VehicleStatuses
                .FirstOrDefaultAsync(s => s.Status == VehicleStatuses.Available, cancellationToken);

            if (newVehicleStatus is null)
                return VehiclesValidationErrors.VehicleStatusNotFound;
            
            var vehicleUpdateResult = await _context.LeasingRequests
                .Include(r => r.Vehicle)
                .Where(r => r.Id == request.Id)
                .ExecuteUpdateAsync(u => u
                        .SetProperty(r => r.Vehicle.StatusId, newVehicleStatus.Id), 
                    cancellationToken);

            if (vehicleUpdateResult < 1)
                return VehiclesValidationErrors.VehicleNotFound;
            
            var deletingResult = await _context.LeasingRequests
                .Where(r => r.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
            
            if (deletingResult < 1)
                return LeasingRequestsValidationErrors.LeasingRequestNotFound;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}