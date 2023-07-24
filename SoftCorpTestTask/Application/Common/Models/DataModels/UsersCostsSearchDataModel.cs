namespace Application.Common.Models.DataModels;

public record UsersCostsSearchDataModel
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<int>? CostCategoriesIds { get; init; }
    public int? UserId { get; init; }
}
