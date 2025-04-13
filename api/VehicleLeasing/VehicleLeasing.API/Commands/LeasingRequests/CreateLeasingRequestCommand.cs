using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Commands.LeasingRequests;

public record CreateLeasingRequestCommand(
    int VehicleId,
    Guid UserId,
    decimal FixedPrice) : IRequest<Result>
{
    public class Handler : IRequestHandler<CreateLeasingRequestCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result> Handle(CreateLeasingRequestCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.VehicleId, cancellationToken);

            if (vehicle is null)
                return VehiclesValidationErrors.VehicleNotFound;
            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
                return UsersValidationErrors.UserNotFound;

            var status = await _context.LeasingRequestStatuses
                .FirstOrDefaultAsync(s => s.Status == LeasingRequestStatuses.Requested, cancellationToken);

            if (status is null)
                return LeasingRequestsValidationErrors.LeasingRequestStatusNotFound;

            var leasingRequestEntity = new LeasingRequest
            {
                VehicleId = request.VehicleId,
                UserId = request.UserId,
                FixedPrice = request.FixedPrice,
                Date = DateOnly.FromDateTime(DateTime.Now),
                StatusId = status.Id
            };

            await _context.AddAsync(leasingRequestEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}