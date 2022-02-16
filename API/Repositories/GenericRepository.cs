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

    public async Task<IReadOnlyCollection<T>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<T> GetByNameAsync(Expression<Func<T, bool>>? filter,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);


        return await query.FirstOrDefaultAsync();
    }

    public void Update(T entity) => _dbSet.Update(entity);

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}

