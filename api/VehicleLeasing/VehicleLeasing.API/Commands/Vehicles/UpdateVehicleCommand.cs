using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Commands.Vehicles.Abstract;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands.Vehicles;

public record UpdateVehicleCommand(
    int Id,
    string Brand,
    string Model,
    int Year,
    decimal EstimatedPrice,
    string Category,
    string Transmission,
    string FuelType,
    string Status)
    : IRequest<Result>, IVehicleCommand
{
    public class Handler : IRequestHandler<UpdateVehicleCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.VehicleCategories
                .FirstOrDefaultAsync(c => c.Category == request.Category, cancellationToken);

            if (category is null) return VehiclesValidationErrors.VehicleCategoryNotFound;
            
            var transmission = await _context.VehicleTransmissions
                .FirstOrDefaultAsync(t => t.Transmission == request.Transmission, cancellationToken);
            
            if (transmission is null) return VehiclesValidationErrors.VehicleTransmissionNotFound;
            
            var fuelType = await _context.VehicleFuelTypes
                .FirstOrDefaultAsync(f => f.Type == request.FuelType, cancellationToken);
            
            if (fuelType is null) return VehiclesValidationErrors.VehicleFuelTypeNotFound;
            
            var status = await _context.VehicleStatuses
                .FirstOrDefaultAsync(s => s.Status == request.Status, cancellationToken);
            
            if (status is null) return VehiclesValidationErrors.VehicleStatusNotFound;

            var updateResult = await _context.Vehicles
                .Where(v => v.Id == request.Id)
                .ExecuteUpdateAsync(u => u
                        .SetProperty(v => v.Brand, request.Brand)
                        .SetProperty(v => v.Model, request.Model)
                        .SetProperty(v => v.Year, request.Year)
                        .SetProperty(v => v.EstimatedPrice, request.EstimatedPrice)
                        .SetProperty(v => v.CategoryId, category.Id)
                        .SetProperty(v => v.TransmissionId, transmission.Id)
                        .SetProperty(v => v.FuelTypeId, fuelType.Id)
                        .SetProperty(v => v.StatusId, status.Id),
                    cancellationToken);
            
            if (updateResult < 1)
                return VehiclesValidationErrors.VehicleNotFound;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}