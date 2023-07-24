using Application.Common.Mappings;
using Application.Common.Models.DataModels;

namespace Application.Common.Models;

public record CostCategoryBriefModel : IMapFrom<CostCategoryBriefDataModel>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Color { get; init; }
}
