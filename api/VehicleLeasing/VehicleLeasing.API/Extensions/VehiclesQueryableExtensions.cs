using System.Linq.Expressions;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.QueryKeys;
using VehicleLeasing.API.Contracts.QueryParameters;
using VehicleLeasing.API.Contracts.QueryParameters.Common;
using VehicleLeasing.API.Contracts.QueryParameters.Vehicles;
using VehicleLeasing.DataAccess.Entities;

namespace VehicleLeasing.API.Extensions;

public static class VehiclesQueryableExtensions
{
    public static IQueryable<Vehicle> Filter(this IQueryable<Vehicle> query, VehiclesFilter? filter)
    {
        if (filter is null)
            return query;
        
        if (!string.IsNullOrEmpty(filter.Brand))
            query = query.Where(x => 
                x.Brand.ToLower().Contains(filter.Brand.ToLower()));

        if (!string.IsNullOrEmpty(filter.Model))
            query = query.Where(x => 
                x.Model.ToLower().Contains(filter.Model.ToLower()));

        if (filter.MinYear.HasValue)
            query = query.Where(x => x.Year >= filter.MinYear.Value);
        
        if (filter.MaxYear.HasValue)
            query = query.Where(x => x.Year <= filter.MaxYear.Value);

        if (filter.MinEstimatedPrice.HasValue)
            query = query.Where(x => x.EstimatedPrice >= filter.MinEstimatedPrice.Value);
        
        if (filter.MaxEstimatedPrice.HasValue)
            query = query.Where(x => x.EstimatedPrice <= filter.MaxEstimatedPrice.Value);

        return query
            .OrderBy(x => x.Id);
    }

    public static IQueryable<Vehicle> OrderByParameters(this IQueryable<Vehicle> query, SortParameters? sortParameters)
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
    
    private static Expression<Func<Vehicle, object>> GetKeySelector(string? orderBy)
    {
        return orderBy switch
        {
            VehicleQueryKeys.Model => x => x.Model,
            VehicleQueryKeys.Year => x => x.Year,
            VehicleQueryKeys.EstimatedPrice => x => x.EstimatedPrice,
            VehicleQueryKeys.LastModified => x => x.LastModified ?? DateTime.MinValue,
            _ => x => x.Brand,
        };
    }
}