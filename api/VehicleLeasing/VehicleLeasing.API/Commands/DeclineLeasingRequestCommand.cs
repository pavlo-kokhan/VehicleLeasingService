using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands;

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
            
            var updateResult = await _context.LeasingRequests
                .Where(r => r.Id == request.Id)
                .ExecuteUpdateAsync(u => u
                        .SetProperty(r => r.StatusId, newLeasingRequestStatus.Id), 
                    cancellationToken);

            if (updateResult < 1)
                return LeasingRequestsValidationErrors.LeasingRequestNotFound;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}