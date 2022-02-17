namespace API.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        ParkRepository = new ParkRepository(_context);
        TrailRepository = new TrailRepository(_context);
    }

    public IParkRepository ParkRepository { get; private set; }

    public ITrailRepository TrailRepository { get; private set; }

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}
