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
            var leasingRequest = await _context.LeasingRequests
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (leasingRequest is null)
                return LeasingRequestsValidationErrors.LeasingRequestNotFound;
            
            var newVehicleStatus = await _context.VehicleStatuses
                .FirstOrDefaultAsync(s => s.Status == VehicleStatuses.Available, cancellationToken);

            if (newVehicleStatus is null)
                return VehiclesValidationErrors.VehicleStatusNotFound;
            
            leasingRequest.Vehicle.StatusId = newVehicleStatus.Id;

            _context.LeasingRequests.Remove(leasingRequest);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}