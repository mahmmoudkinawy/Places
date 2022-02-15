namespace API.Repositories;

//I did this class just if I wanna add some thing especial to it like RemoveRange ... etc
public class ParkRepository : GenericRepository<Park>, IParkRepository
{
    public ParkRepository(DataContext context) : base(context)
    { }
}