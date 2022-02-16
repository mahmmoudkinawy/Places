namespace API.Controllers;
public class TrailsController : BaseApiController
{
    private readonly ITrailRepository _trailRepository;
    private readonly IMapper _mapper;

    public TrailsController(ITrailRepository trailRepository, IMapper mapper)
    {
        _trailRepository = trailRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<Trail>>> GetAllTrailsAsync()
        => Ok(await _trailRepository.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> CreateTrailAsync([FromBody] TrailCreateDto trailCreateDto)
    {
        if (trailCreateDto == null) return BadRequest();

        var trailFromRepo = _trailRepository
                .GetByNameAsync(t => t.Name.ToLower() == trailCreateDto.Name.ToLower());

        if(trailFromRepo == null) return BadRequest();

        var createdTrail = _mapper.Map<Trail>(trailCreateDto);

        _trailRepository.Add(createdTrail);
        await _trailRepository.SaveChangesAsync();

        return Ok("Created");
    }
}
