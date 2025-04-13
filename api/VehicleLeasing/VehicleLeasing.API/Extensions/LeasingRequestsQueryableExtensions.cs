using System.Linq.Expressions;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.QueryKeys;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.LeasingRequests;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Extensions;

public static class LeasingRequestsQueryableExtensions
{
    public static IQueryable<LeasingRequest> Filter(this IQueryable<LeasingRequest> query, LeasingRequestsFilter? filter)
    {
        if (filter is null)
            return query;
        
        if (!string.IsNullOrEmpty(filter.VehicleBrand))
            query = query.Where(x => 
                x.Vehicle.Brand.ToLower().Contains(filter.VehicleBrand.ToLower()));

        if (!string.IsNullOrEmpty(filter.VehicleModel))
            query = query.Where(x => 
                x.Vehicle.Model.ToLower().Contains(filter.VehicleModel.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.UserName))
            query = query.Where(x => 
                x.User.Name.ToLower().Contains(filter.UserName.ToLower()));
        
        if (filter.MinDate.HasValue)
            query = query.Where(x => x.Date >= filter.MinDate.Value);
        
        if (filter.MaxDate.HasValue)
            query = query.Where(x => x.Date <= filter.MaxDate.Value);
        
        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(x 
                => x.Status.Status.ToLower().Contains(filter.Status.ToLower()));
        
        if (filter.MinLastModified.HasValue)
            query = query.Where(x => x.LastModified >= filter.MinLastModified.Value);
        
        if (filter.MaxLastModified.HasValue)
            query = query.Where(x => x.LastModified <= filter.MaxLastModified.Value);
        
        return query
            .OrderBy(x => x.Id);
    }
    
    public static IQueryable<LeasingRequest> OrderByParameters(this IQueryable<LeasingRequest> query, SortParameters? sortParameters)
    {
        if (sortParameters is null 
            || string.IsNullOrEmpty(sortParameters.OrderBy) 
            || !sortParameters.Ascending.HasValue)
        {
            return query;
        }
        
        var orderBy = sortParameters.OrderBy?.ToLowerInvariant();
        return sortParameters.Ascending.Value
            ? query.OrderBy(GetKeySelector(orderBy)) 
            : query.OrderByDescending(GetKeySelector(orderBy));
    }
    
    private static Expression<Func<LeasingRequest, object>> GetKeySelector(string? orderBy)
    {
        return orderBy switch
        {
            LeasingRequestQueryKeys.VehicleBrand => x => x.Vehicle.Brand,
            LeasingRequestQueryKeys.VehicleModel => x => x.Vehicle.Model,
            LeasingRequestQueryKeys.UserName => x => x.User.Name,
            LeasingRequestQueryKeys.FixedPrice => x => x.FixedPrice,
            LeasingRequestQueryKeys.Date => x => x.Date,
            LeasingRequestQueryKeys.LastModified => x => x.LastModified ?? DateTime.MinValue,
            _ => x => x.Status,
        };
    }
}