using Application.Common.Mappings;
using Application.Common.Models.DataModels.UsersCostStatistic;

namespace Application.Common.Models.UsersCostStatistic;

public record UsersCostsStatisticModel : IMapFrom<UsersCostsStatisticDataModel>
{
    public string CostCategoryName { get; init; }
    public decimal TotalCostSum { get; init; }
    public decimal PercentageOfTotalCosts { get; init; }
}