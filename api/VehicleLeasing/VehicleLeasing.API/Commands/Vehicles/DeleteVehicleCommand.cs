using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Commands.Vehicles;

public record DeleteVehicleCommand(int Id) : IRequest<Result>
{
    public class Handler : IRequestHandler<DeleteVehicleCommand, Result>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicleEntity = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vehicleEntity is null)
                return VehiclesValidationErrors.VehicleNotFound;

            await _context.Vehicles
                .Where(v => v.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}