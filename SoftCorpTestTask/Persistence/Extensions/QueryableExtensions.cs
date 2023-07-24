using Application.Common.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var paginatedList = new PaginatedList<T>(items, count, pageNumber, pageSize);
        return paginatedList;
    }
}