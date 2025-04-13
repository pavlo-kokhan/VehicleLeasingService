using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Common;
using VehicleLeasing.API.Contracts.LeasingRequests;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.LeasingRequests;
using VehicleLeasing.API.Contracts.Users;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Extensions;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.LeasingRequests;

public record LeasingRequestsPageQuery(
    LeasingRequestsFilter? Filter,
    SortParameters? SortParameters,
    PageParameters? PageParameters) : IRequest<Result<PagedResponse<LeasingRequestResponse>>>
{
    public class Handler : IRequestHandler<LeasingRequestsPageQuery, Result<PagedResponse<LeasingRequestResponse>>>
    {
        private readonly VehicleLeasingDbContext _context;

        public Handler(VehicleLeasingDbContext context)
        {
            _context = context;
        }
        
        public async Task<Result<PagedResponse<LeasingRequestResponse>>> Handle(LeasingRequestsPageQuery request, CancellationToken cancellationToken)
        {
            var paginatedEntities = await _context.LeasingRequests
                .Include(r => r.Vehicle)
                .Include(r => r.Vehicle.Category)
                .Include(r => r.Vehicle.Transmission)
                .Include(r => r.Vehicle.FuelType)
                .Include(r => r.Vehicle.Status)
                .Include(r => r.User)
                .Include(r => r.User.Role)
                .Include(r => r.Status)
                .Filter(request.Filter)
                .OrderByParameters(request.SortParameters)
                .ToPagedAsync(request.PageParameters);

            return new PagedResponse<LeasingRequestResponse>(
                paginatedEntities.Items
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
                            r.Vehicle.Status.Status,
                            r.Vehicle.LastModified),
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
                        r.LastModified)),
                paginatedEntities.PageNumber,
                paginatedEntities.PageSize,
                paginatedEntities.TotalCount
            );
        }
    }
}