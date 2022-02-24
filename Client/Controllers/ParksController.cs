using Client.Helpers;
namespace Client.Controllers;
public class ParksController : Controller
{
    private readonly IParkRepository _parkRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public ParksController(IParkRepository parkRepository,
        IHttpClientFactory httpClientFactory)
    {
        _parkRepository = parkRepository;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index() => View();

    public async Task<IActionResult> Upsert(int? id)
    {
        var park = new Park();

        if (id == null) return View(park);

        park = await _parkRepository.GetAsync(Constants.ParksPath, id.GetValueOrDefault());

        if (park == null) return NotFound();

        return View(park);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Park park)
    {
        if (ModelState.IsValid)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                byte[] p1 = null;
                using var fs1 = files[0].OpenReadStream();
                using var ms1 = new MemoryStream();
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();

                park.Picture = p1;
            }
            else
            {
                var parkFromDb = await _parkRepository.GetAsync(Constants.ParksPath, park.Id);
                park.Picture = parkFromDb.Picture;
            }

            if (park.Id == 0)
            {
                _parkRepository.Create(Constants.ParksPath, park);
            }
            else
            {
                _parkRepository.Update(Constants.ParksPath + park.Id, park);
            }

            return RedirectToAction(nameof(Index));
        }

        return View(park);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParks()
        => Json(new { data = await _parkRepository.GetAllAsync(Constants.ParksPath) });

}
