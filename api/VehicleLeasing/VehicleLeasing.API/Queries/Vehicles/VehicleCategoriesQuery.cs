using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleCategoriesQuery : IRequest<Result<List<VehicleCategoryResponse>>>
{
    public class Handler : IRequestHandler<VehicleCategoriesQuery, Result<List<VehicleCategoryResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<VehicleCategoryResponse>>> Handle(VehicleCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.VehicleCategories
                .OrderBy(c => c.Category)
                .Select(c => new VehicleCategoryResponse(c.Id, c.Category))
                .ToListAsync(cancellationToken);
        }
    }
}