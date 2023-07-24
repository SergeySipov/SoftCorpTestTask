using Application.Interfaces.UnitOfWork;
using Persistence.DbContexts;

namespace Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly SoftCorpTestTaskDbContext _dbContext;

    public UnitOfWork(SoftCorpTestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task RollbackAsync()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
