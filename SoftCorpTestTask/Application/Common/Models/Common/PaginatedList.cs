namespace Application.Common.Models.Common;

public record PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; init; }
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }
    public int TotalCount { get; init; }

    public PaginatedList()
    {
    }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }
}