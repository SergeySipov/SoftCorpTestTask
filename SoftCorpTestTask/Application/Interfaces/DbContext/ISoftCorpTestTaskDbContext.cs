namespace Application.Interfaces.DbContext;

public interface ISoftCorpTestTaskDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}