using Domain.Common.Interfaces;

namespace Application.Interfaces.DbContextSeed;

public interface IDbContextSeed<T> where T : class, IEntity
{
    
}