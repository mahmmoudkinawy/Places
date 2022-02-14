namespace API.Repositories;
public class ParkRepository : GenericRepository<Park>
{
    public ParkRepository(DataContext context) : base(context)
    { }
}
