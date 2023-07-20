using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models;

public record CostCategoryModel : IMapFrom<CostCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
}