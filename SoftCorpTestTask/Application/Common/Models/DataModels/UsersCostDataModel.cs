namespace Application.Common.Models.DataModels;

public record UsersCostDataModel
{
    public decimal Price { get; init; }
    public string Comment { get; init; }
    public DateTime Date { get; init; }
    public string CostCategoryName { get; init; }
    public string CostCategoryColor { get; init; }
}