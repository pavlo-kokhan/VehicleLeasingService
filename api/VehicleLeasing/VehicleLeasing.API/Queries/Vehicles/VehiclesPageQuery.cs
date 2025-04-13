using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Common;
using VehicleLeasing.API.Contracts.QueryParameters;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.Vehicles;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehiclesPageQuery(
    VehiclesFilter? Filter, 
    SortParameters? SortParameters,
    PageParameters? PageParameters) : IRequest<Result<PagedResponse<VehicleResponse>>>
{
    public class Handler : IRequestHandler<VehiclesPageQuery, Result<PagedResponse<VehicleResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedResponse<VehicleResponse>>> Handle(VehiclesPageQuery request, CancellationToken cancellationToken)
        {
            var paginatedEntities = await _context.Vehicles
                .Include(v => v.Category)
                .Include(v => v.Transmission)
                .Include(v => v.FuelType)
                .Include(v => v.Status)
                .Filter(request.Filter)
                .OrderByParameters(request.SortParameters)
                .ToPagedAsync(request.PageParameters);

            return new PagedResponse<VehicleResponse>(
                paginatedEntities.Items
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
                        v.LastModified)), 
                paginatedEntities.PageNumber,
                paginatedEntities.PageSize,
                paginatedEntities.TotalCount);
        }
    }
}