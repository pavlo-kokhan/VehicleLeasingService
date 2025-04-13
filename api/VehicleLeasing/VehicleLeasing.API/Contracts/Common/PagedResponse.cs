namespace VehicleLeasing.API.Contracts.Common;

public class PagedResponse<T>(
    IEnumerable<T> items,
    int pageNumber,
    int pageSize,
    int totalCount)
{
    public IEnumerable<T> Items { get; } = items;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
}