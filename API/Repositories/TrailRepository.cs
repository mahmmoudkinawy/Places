namespace API.Repositories;
public class TrailRepository : GenericRepository<Trail>, ITrailRepository
{
    private readonly DataContext _context;

    public TrailRepository(DataContext context) : base(context) => _context = context;

    public async Task<IReadOnlyCollection<Trail>> GetTrailsInParkAsync(int parkId) =>
     await _context.Trails.Include(p => p.Park).Where(p => p.ParkId == parkId).ToListAsync();

}