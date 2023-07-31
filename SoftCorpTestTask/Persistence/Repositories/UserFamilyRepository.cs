using Application.Common.Models.DataModels.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class UserFamilyRepository : IUserFamilyRepository
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public UserFamilyRepository(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UserFamily> GetAsync(int userId, int familyId)
    {
        var userFamily = (from uf in _dbContext.UsersFamilies
            where uf.UserId == userId &&
                  uf.FamilyId == familyId
            select uf).FirstOrDefaultAsync();

        return userFamily;
    }

    public IAsyncEnumerable<UserBriefDataModel> GetFamilyMembersAsync(int familyId)
    {
        var userBriefModels = (from uf in _dbContext.UsersFamilies
            join u in _dbContext.Users on uf.UserId equals u.Id
            where uf.FamilyId == familyId
            select new UserBriefDataModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username
            }).AsAsyncEnumerable();

        return userBriefModels;
    }
}
