using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.LeasingRequests;
using VehicleLeasing.API.Contracts.Users;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.LeasingRequests;

public record LeasingRequestQuery(int Id) : IRequest<Result<LeasingRequestResponse>>
{
    public class Handler : IRequestHandler<LeasingRequestQuery, Result<LeasingRequestResponse>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<LeasingRequestResponse>> Handle(LeasingRequestQuery request, CancellationToken cancellationToken)
        {
            var leasingRequest = await _context.LeasingRequests
                .Include(r => r.Vehicle)
                .Include(r => r.User)
                .Include(r => r.Status)
                .Where(r => r.Id == request.Id)
                .Select(r => new LeasingRequestResponse(
                    r.Id,
                    new VehicleResponse(
                        r.Vehicle.Id,
                        r.Vehicle.Brand,
                        r.Vehicle.Model,
                        r.Vehicle.Year,
                        r.Vehicle.EstimatedPrice,
                        r.Vehicle.Category.Category,
                        r.Vehicle.Transmission.Transmission,
                        r.Vehicle.FuelType.Type,
                        r.Status.Status,
                        r.LastModified),
                    new UserResponse(
                        r.User.Id,
                        r.User.Role.Name,
                        r.User.Name,
                        r.User.Surname,
                        r.User.Email,
                        r.User.PasswordHash),
                    r.FixedPrice,
                    r.Date,
                    r.Status.Status,
                    r.LastModified))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (leasingRequest is null) return LeasingRequestsValidationErrors.LeasingRequestNotFound;

            return leasingRequest;
        }
    }
}