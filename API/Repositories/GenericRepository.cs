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

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Add(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

}

