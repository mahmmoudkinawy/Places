namespace Client.Repositories;
public class ParkRepository : GenericRepository<Park>, IParkRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ParkRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => _httpClientFactory = httpClientFactory;
}
