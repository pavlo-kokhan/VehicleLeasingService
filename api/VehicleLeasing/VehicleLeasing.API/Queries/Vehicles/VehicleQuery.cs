using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleQuery(int Id) : IRequest<Result<VehicleResponse>>
{
    public class Handler : IRequestHandler<VehicleQuery, Result<VehicleResponse>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<VehicleResponse>> Handle(VehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles
                .AsNoTracking()
                .Include(v => v.Category)
                .Include(v => v.Transmission)
                .Include(v => v.FuelType)
                .Include(v => v.Status)
                .Where(v => v.Id == request.Id)
                .Select(v => new VehicleResponse(
                    v.Id,
                    v.Brand,
                    v.Model,
                    v.Year,
                    v.EstimatedPrice,
                    v.Category.Category,
                    v.Transmission.Transmission,
                    v.FuelType.Type,
                    v.Status.Status,
                    v.LastModified))
                .FirstOrDefaultAsync(cancellationToken);

            if (vehicle is null) return VehiclesValidationErrors.VehicleNotFound;
            
            return vehicle;
        }
    }
}