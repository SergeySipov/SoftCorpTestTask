using Application.Common.Models;
using Application.Common.Models.DataModels;

namespace Application.Interfaces.Repositories;

public interface ICostCategoryRepository
{
    Task<bool> IsCostCategoryInUse(int id);
    Task<CostCategoryBriefDataModel> GetByIdAsync(int id);
}