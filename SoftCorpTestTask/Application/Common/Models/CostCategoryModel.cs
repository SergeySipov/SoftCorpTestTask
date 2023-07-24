using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models;

public record CostCategoryModel : IMapFrom<CostCategory>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Color { get; init; }
}