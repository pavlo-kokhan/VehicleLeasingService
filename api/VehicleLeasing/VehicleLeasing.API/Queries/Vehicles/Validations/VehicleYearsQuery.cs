using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles.Validations;

public record VehicleYearsQuery(
    string Category, 
    string Brand, 
    string Model, 
    string Contains) : IRequest<Result<List<int>>>
{
    public class Handler : IRequestHandler<VehicleYearsQuery, Result<List<int>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<List<int>>> Handle(VehicleYearsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.VehicleCategories
                .FirstOrDefaultAsync(c => c.Category == request.Category, cancellationToken);

            if (category is null) 
                return VehiclesValidationErrors.VehicleCategoryNotFound;
            
            var query = _context.Vehicles
                .Where(v => v.Status.Status == VehicleStatuses.Available
                            && v.Category.Category == request.Category
                            && v.Brand == request.Brand
                            && v.Model == request.Model);
            
            if (!string.IsNullOrWhiteSpace(request.Contains))
            {
                query = query.Where(v => v.Model.ToLower().Replace(" ", string.Empty)
                    .Contains(request.Contains.ToLower().Replace(" ", string.Empty)));
            }
            
            return await query
                .Select(v => v.Year)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}