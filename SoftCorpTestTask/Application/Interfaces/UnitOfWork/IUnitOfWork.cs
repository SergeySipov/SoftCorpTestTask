namespace Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken cancellationToken);
    Task RollbackAsync();
}