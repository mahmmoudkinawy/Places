namespace API.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Add(T entity) => _dbSet.Add(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<IReadOnlyCollection<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetAsyncById(int id) => await _dbSet.FindAsync(id);

    public async Task<T> GetAsyncByName(Expression<Func<T, bool>>? filter, string name)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public void Update(T entity) => _dbSet.Update(entity);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}

