namespace API.Interfaces;
public interface ITrailRepository : IGenericRepository<Trail>
{
    Task<IReadOnlyCollection<Trail>> GetTrailsInParkAsync(int parkId);
}
