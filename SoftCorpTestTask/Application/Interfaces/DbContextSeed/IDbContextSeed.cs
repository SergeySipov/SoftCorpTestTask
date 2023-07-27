namespace Application.Interfaces.DbContextSeed;

public interface IDbContextSeed : IDisposable 
{
    void InitDbWithDefaultValues();
}