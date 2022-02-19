using Client.Helpers;
using Newtonsoft.Json;

namespace Client.Controllers;
public class ParksController : Controller
{
    private readonly IParkRepository _parkRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public ParksController(IParkRepository parkRepository, IHttpClientFactory httpClientFactory)
    {
        _parkRepository = parkRepository;
        _httpClientFactory = httpClientFactory;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParks()
    {
        return Json(new { data = await _parkRepository.GetAllAsync(Constants.ParksPath) });
    }
}
