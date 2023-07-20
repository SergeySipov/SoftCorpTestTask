using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models;

public record CostCategoryBriefModel : IMapFrom<CostCategory>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
}
