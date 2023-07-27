using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    private readonly SoftCorpTestTaskDbContext _testDbContext;

    public UserRefreshTokenRepository(SoftCorpTestTaskDbContext testDbContext)
    {
        _testDbContext = testDbContext;
    }

    public Task<UserRefreshToken?> GetUserRefreshTokenAsync(Guid token)
    {
        return (from t in _testDbContext.UsersRefreshTokens
                where t.Token == token
                select t).FirstOrDefaultAsync();
    }

    public Task<UserRefreshToken?> GetUserRefreshTokenAsync(int userId)
    {
        return (from t in _testDbContext.UsersRefreshTokens
            where t.UserId == userId
            select t).FirstOrDefaultAsync();
    }
}