using Application.Common.Models.Common;
using Application.Common.Models.DataModels;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Persistence.DbContexts;
using System.Linq.Expressions;
using Persistence.Extensions;
using Application.Common.Models.DataModels.UsersCostStatistic;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UsersCostRepository : IUsersCostRepository
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public UsersCostRepository(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PaginatedList<UsersCostDataModel>> GetUsersCostsByMonthAsync(UsersCostsSearchDataModel searchModel,
        Month month)
    {
        var query = ApplyFiltersToQuery(_dbContext.UsersCosts,
            searchModel.UserId,
            searchModel.CostCategoriesIds,
            uc => uc.Date.Month == (int)month);

        var usersCostDataModels = from uc in query
            join cc in _dbContext.CostCategories on uc.CostCategoryId equals cc.Id
            select new UsersCostDataModel
            {
                Date = uc.Date,
                Comment = uc.Comment,
                Price = uc.Price,
                CostCategoryName = cc.Name,
                CostCategoryColor = cc.Color
            };

        return usersCostDataModels.ToPaginatedListAsync(searchModel.PageNumber, searchModel.PageSize);
    }

    public async Task<FamilyCostsStatisticDataModel> GetFamilyCostsStatisticByPeriodAsync(
        UsersCostsSearchDataModel searchModel,
        DateTime startDate,
        DateTime endDate)
    {
        var currentFamily = await (from uf in _dbContext.UsersFamilies
            join f in _dbContext.Families on uf.FamilyId equals f.Id
            where uf.UserId == searchModel.UserId
            select new
            {
                f.Id,
                f.Title
            }).FirstOrDefaultAsync();

        var selectedUserFamilyInfo = await (from u in _dbContext.Users
            join uf in _dbContext.UsersFamilies on u.Id equals uf.UserId into userFamilies
            from uf in userFamilies.DefaultIfEmpty()
            where (currentFamily == null && u.Id == searchModel.UserId) ||
                  (currentFamily != null && uf.FamilyId == currentFamily.Id)
            select new
            {
                UserId = u.Id,
                u.Username,
                u.FirstName,
                u.LastName
            }).ToListAsync();

        var familyMemberCostsStatistic = new List<FamilyMemberCostsStatisticDataModel>(selectedUserFamilyInfo.Count);
        foreach (var familyMember in selectedUserFamilyInfo)
        {
            var familyMemberBriefStatistic = await GetFamilyMemberCostsStatisticByPeriodAsync(
                searchModel with { UserId = familyMember.UserId }, startDate, endDate);

            var familyMemberStatistic = new FamilyMemberCostsStatisticDataModel
            {
                FirstName = familyMember.FirstName,
                LastName = familyMember.LastName,
                Username = familyMember.Username,
                FamilyMemberDetailedStatistic = familyMemberBriefStatistic.FamilyMemberDetailedStatistic,
                TotalCosts = familyMemberBriefStatistic.TotalCosts
            };

            familyMemberCostsStatistic.Add(familyMemberStatistic);
        }

        var familyCostsStatistic = new FamilyCostsStatisticDataModel
        {
            FamilyTitle = currentFamily?.Title ?? string.Empty,
            FamilyMemberStatistic = familyMemberCostsStatistic
        };

        return familyCostsStatistic;
    }

    private async Task<FamilyMemberCostsBriefStatisticDataModel> GetFamilyMemberCostsStatisticByPeriodAsync(
        UsersCostsSearchDataModel searchModel,
        DateTime startDate,
        DateTime endDate)
    {
        var query = ApplyFiltersToQuery(_dbContext.UsersCosts,
            searchModel.UserId,
            searchModel.CostCategoriesIds,
            uc => uc.Date >= startDate && uc.Date <= endDate);

        var totalCosts = query.Sum(x => x.Price);

        var usersCostsStatisticDataModels = from uc in query
            join cc in _dbContext.CostCategories on uc.CostCategoryId equals cc.Id
            group uc by cc.Name
            into guc
            select new UsersCostsStatisticDataModel
            {
                CostCategoryName = guc.Key,
                TotalCostSum = guc.Sum(x => x.Price),
                PercentageOfTotalCosts = guc.Sum(x => x.Price) / totalCosts * 100
            };

        var statisticPaginatedList =
            await usersCostsStatisticDataModels.ToPaginatedListAsync(searchModel.PageNumber, searchModel.PageSize);
        
        var familyMemberStatistic = new FamilyMemberCostsBriefStatisticDataModel
        {
            FamilyMemberDetailedStatistic = statisticPaginatedList,
            TotalCosts = totalCosts
        };

        return familyMemberStatistic;
    }

    private IQueryable<UsersCost> ApplyFiltersToQuery(IQueryable<UsersCost> query,
        int? userId,
        IEnumerable<int>? costCategoriesIds,
        Expression<Func<UsersCost, bool>>? extendedFilter = null)
    {
        if (userId.HasValue)
        {
            query = query.Where(uc => uc.UserId == userId);
        }

        if (costCategoriesIds != null && costCategoriesIds.Any())
        {
            query = query.Where(uc => costCategoriesIds.Contains(uc.CostCategoryId));
        }

        if (extendedFilter != null)
        {
            query = query.Where(extendedFilter);
        }

        return query;
    }
}