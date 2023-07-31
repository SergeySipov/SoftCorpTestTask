using Application.Common.Models.DataModels.User;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserFamilyRepository
{
    public Task<UserFamily> GetAsync(int userId, int familyId);
    public IAsyncEnumerable<UserBriefDataModel> GetFamilyMembersAsync(int familyId);
}