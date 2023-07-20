namespace Application.Common.Models.DataModels;

public record UsersCostDataModel
{
    public decimal Price { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public string CostCategoryName { get; set; }
}