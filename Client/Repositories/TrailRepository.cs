namespace Client.Repositories;
public class TrailRepository : GenericRepository<Trail>, ITrailRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    public TrailRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        => _httpClientFactory = httpClientFactory;
}