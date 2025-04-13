using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Contracts.Common;
using VehicleLeasing.API.Contracts.QueryParameters.Common;

namespace VehicleLeasing.API.Extensions;

public static class CommonQuaryableExtensions
{
    public static async Task<PagedResponse<T>> ToPagedAsync<T>(this IQueryable<T> query, PageParameters? pageParameters)
    { 
        if (pageParameters is null)
            pageParameters = new PageParameters();
        
        var totalCount = await query.CountAsync();

        if (totalCount == 0)
            return new PagedResponse<T>([], 1, 0, 0);
        
        var skip = (pageParameters.PageNumber - 1) * pageParameters.PageSize;
        
        var items = await query
            .Skip(skip)
            .Take(pageParameters.PageSize)
            .ToListAsync();

        return new PagedResponse<T>(items, pageParameters.PageNumber, pageParameters.PageSize, totalCount);
    } 
}