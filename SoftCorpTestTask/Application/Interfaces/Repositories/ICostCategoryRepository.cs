using Application.Common.Models;

namespace Application.Interfaces.Repositories;

public interface ICostCategoryRepository
{
    ValueTask<bool> IsCostCategoryInUse(int id);
    Task<CostCategoryBriefModel> GetByIdAsync(int id);
}