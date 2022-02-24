namespace API.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync(string? includeProperties = null);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
    Task<T> GetByNameAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}