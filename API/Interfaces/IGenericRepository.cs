namespace API.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> GetByNameAsync(Expression<Func<T, bool>>? filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}