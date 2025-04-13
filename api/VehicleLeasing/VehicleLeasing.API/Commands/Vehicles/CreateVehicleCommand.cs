using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Commands.Vehicles.Abstract;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Commands.Vehicles;

public record CreateVehicleCommand(
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
    public class Handler : IRequestHandler<CreateVehicleCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.VehicleCategories
                .FirstOrDefaultAsync(c => c.Category == request.Category, cancellationToken);

            if (category is null) 
                return VehiclesValidationErrors.VehicleCategoryNotFound;
            
            var transmission = await _context.VehicleTransmissions
                .FirstOrDefaultAsync(t => t.Transmission == request.Transmission, cancellationToken);
            
            if (transmission is null) 
                return VehiclesValidationErrors.VehicleTransmissionNotFound;
            
            var fuelType = await _context.VehicleFuelTypes
                .FirstOrDefaultAsync(f => f.Type == request.FuelType, cancellationToken);
            
            if (fuelType is null) 
                return VehiclesValidationErrors.VehicleFuelTypeNotFound;
            
            var status = await _context.VehicleStatuses
                .FirstOrDefaultAsync(s => s.Status == request.Status, cancellationToken);
            
            if (status is null) 
                return VehiclesValidationErrors.VehicleStatusNotFound;

            var vehicleEntity = new Vehicle
            {
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                EstimatedPrice = request.EstimatedPrice,
                CategoryId = category.Id,
                TransmissionId = transmission.Id,
                FuelTypeId = fuelType.Id,
                StatusId = status.Id
            };
            
            await _context.Vehicles.AddAsync(vehicleEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}