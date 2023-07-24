using Application.Common.Models.Common;
using Application.Common.Models.DataModels;
using Application.Common.Models.DataModels.UsersCostStatistic;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IUsersCostRepository
{
    Task<PaginatedList<UsersCostDataModel>> GetUsersCostsByMonthAsync(UsersCostsSearchDataModel searchModel, Month month);

    Task<FamilyCostsStatisticDataModel> GetFamilyCostsStatisticByPeriodAsync(UsersCostsSearchDataModel searchModel,
        DateTime startDate,
        DateTime endDate);
}