using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models.DataModels;

public record CostCategoryBriefDataModel : IMapFrom<CostCategory>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Color { get; init; }
}
