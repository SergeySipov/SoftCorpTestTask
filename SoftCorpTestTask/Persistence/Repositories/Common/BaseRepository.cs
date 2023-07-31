using Application.Interfaces.Repositories.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence.DbContexts;
using Persistence.Exceptions;

namespace Persistence.Repositories.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public BaseRepository(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var existedEntity = await _dbContext.Set<T>().FindAsync(entity.Id);

        if (existedEntity == null)
        {
            throw new EntityNotFoundException(typeof(T), entity.Id.ToString());
        }

        _dbContext.Entry(existedEntity).CurrentValues.SetValues(entity);
        _dbContext.Entry(existedEntity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Delete(int id)
    {
        var entity = Activator.CreateInstance<T>();
        entity.Id = id;

        var set = _dbContext.Set<T>();

        set.Attach(entity);
        set.Remove(entity);
    }

    public IAsyncEnumerable<T> GetAllAsync()
    {
        return _dbContext
            .Set<T>()
            .AsAsyncEnumerable();
    }

    public ValueTask<T> GetByIdAsync(int id)
    {
        return _dbContext.Set<T>().FindAsync(id)!;
    }

    public Task ClearTableAsync()
    {
        var set = _dbContext.Set<T>();

        var entityType = _dbContext.Model.FindEntityType(set.GetType().GenericTypeArguments[0]);

        var query = @$"DELETE FROM [{entityType.GetSchema() ?? "dbo"}].[{entityType.GetTableName()}];";
        return _dbContext.Database.ExecuteSqlRawAsync(query);
    }
}
