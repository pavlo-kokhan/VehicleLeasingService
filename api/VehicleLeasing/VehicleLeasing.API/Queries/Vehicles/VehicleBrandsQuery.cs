using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleBrandsQuery(string Category, string Contains) : IRequest<Result<List<string>>>
{
    public class Handler : IRequestHandler<VehicleBrandsQuery, Result<List<string>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<string>>> Handle(VehicleBrandsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.VehicleCategories
                .FirstOrDefaultAsync(c => c.Category == request.Category, cancellationToken);
            
            if (category is null) 
                return VehiclesValidationErrors.VehicleCategoryNotFound;
            
            var query = _context.Vehicles
                .Where(v => v.Status.Status == VehicleStatuses.Available
                            && v.Category.Category == request.Category);

            if (!string.IsNullOrWhiteSpace(request.Contains))
                query = query.Where(v => v.Brand.ToLower().Replace(" ", string.Empty)
                    .Contains(request.Contains.ToLower().Replace(" ", string.Empty)));

            return await query
                .Select(v => v.Brand)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}