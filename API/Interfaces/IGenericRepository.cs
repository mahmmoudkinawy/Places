namespace API.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync(string? includeProperties = null);
    Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
    Task<T> GetByNameAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}