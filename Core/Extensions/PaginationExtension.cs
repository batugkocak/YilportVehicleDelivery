using System.Collections;

namespace Core.Extensions;

public static class PaginationExtension
{
    public static PagingResponse<T> ToPaginate<T>(this IQueryable<T> source, PaginationRequestParameters parameters)
    {
        var dataCount = source.Count();
        var items = source.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize).ToList();
        return new PagingResponse<T>
        {
            Items = items,
            TotalCount = dataCount,
        };
    }
}

public class  PaginationRequestParameters
{
    public int PageIndex {
        get;
        set;
    }

    public int PageSize { get; set; }
}

public class PagingResponse<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
}