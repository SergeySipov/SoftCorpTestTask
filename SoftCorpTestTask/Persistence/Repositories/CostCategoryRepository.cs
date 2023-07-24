using Application.Common.Models.DataModels;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class CostCategoryRepository : ICostCategoryRepository
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public CostCategoryRepository(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsCostCategoryInUse(int id)
    {
        return _dbContext.CostCategories.AnyAsync(e => e.Id == id);
    }

    public Task<CostCategoryBriefDataModel> GetByIdAsync(int id)
    {
        var model = (from cc in _dbContext.CostCategories
            where cc.Id == id
            select new CostCategoryBriefDataModel
            {
                Name = cc.Name,
                Color = cc.Color,
                Description = cc.Description,
            }).FirstOrDefaultAsync();

        return model;
    }
}