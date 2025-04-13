using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleCategoryQuery(int Id) : IRequest<Result<VehicleCategoryResponse>>
{
    public class Handler : IRequestHandler<VehicleCategoryQuery, Result<VehicleCategoryResponse>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<VehicleCategoryResponse>> Handle(VehicleCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.VehicleCategories
                .Where(c => c.Id == request.Id)
                .Select(c => new VehicleCategoryResponse(c.Id, c.Category))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (category is null) return VehiclesValidationErrors.VehicleCategoryNotFound;
            
            return category;
        }
    }
}