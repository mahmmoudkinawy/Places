namespace API.Repositories;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;

    private bool _disposed = false;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        ParkRepository = new ParkRepository(_context);
        TrailRepository = new TrailRepository(_context);
    }

    public IParkRepository ParkRepository { get; private set; }

    public ITrailRepository TrailRepository { get; private set; }

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed) //true
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
