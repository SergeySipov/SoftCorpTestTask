using MediatR;

namespace Application.UseCases.UsersCostFeatures.Queries.Common;

public record UsersCostsSearchQueryBaseRequest<T> : IRequest<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<int>? CostCategoriesIds { get; init; }
    public int? UserId { get; init; }
}