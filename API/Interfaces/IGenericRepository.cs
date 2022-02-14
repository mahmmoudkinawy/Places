namespace API.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<T> GetAsyncById(int id);
    Task<T> GetAsyncByName(Expression<Func<T, bool>>? filter,string name);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}

