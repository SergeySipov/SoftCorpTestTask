﻿using Domain.Common.Interfaces;

namespace Application.Interfaces.Repositories.Common;

public interface IBaseRepository<T> where T : class, IEntity
{
    ValueTask<T> GetByIdAsync(int id);
    IAsyncEnumerable<T> GetAllAsync();
    void Add(T entity);
    Task UpdateAsync(T entity);
    void Delete(T entity);
    void Delete(int id);
    Task ClearTableAsync();
}