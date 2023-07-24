namespace Application.Common.Models.DataModels.UsersCostStatistic;

public record FamilyCostsStatisticDataModel
{
    public string FamilyTitle { get; init; }
    public IReadOnlyCollection<FamilyMemberCostsStatisticDataModel> FamilyMemberStatistic { get; init; }
}
