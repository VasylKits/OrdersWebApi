using Application.Common.Result.Abstractions.Generics;

namespace Interfaces.Repositories;

public interface IBaseRepository<T>
{
    Task<IResult<T>> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}