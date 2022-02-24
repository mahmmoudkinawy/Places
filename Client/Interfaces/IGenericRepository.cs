namespace Client.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(string url);
    Task<T> GetAsync(string url, int id);
    bool Create(string url, T entity);
    bool Update(string url, T entity);
    bool Delete(string url, int id);
}