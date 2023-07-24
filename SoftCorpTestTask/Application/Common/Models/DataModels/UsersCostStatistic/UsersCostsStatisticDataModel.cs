namespace Application.Common.Models.DataModels.UsersCostStatistic;

public record UsersCostsStatisticDataModel
{
    public string CostCategoryName { get; init; }
    public decimal TotalCostSum { get; init; }
    public decimal PercentageOfTotalCosts { get; init; }
}