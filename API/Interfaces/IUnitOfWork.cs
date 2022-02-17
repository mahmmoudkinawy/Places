namespace API.Interfaces;
public interface IUnitOfWork
{
    IParkRepository ParkRepository { get; }
    ITrailRepository TrailRepository { get; }
    Task<bool> SaveChangesAsync();
}