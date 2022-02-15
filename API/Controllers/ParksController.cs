namespace API.Controllers;
public class ParksController : BaseApiController
{
    private readonly IParkRepository _parkRepository;
    private readonly IMapper _mapper;

    public ParksController(IParkRepository parkRepository, IMapper mapper)
    {
        _parkRepository = parkRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ParkDto>>> GetAllParksAsync() =>
        Ok(_mapper.Map<IReadOnlyCollection<ParkDto>>(await _parkRepository.GetAllAsync()));

    [HttpGet("{id}")]
    public async Task<ActionResult<ParkDto>> GetParkAsync([FromRoute] int id)
    {
        var park = await _parkRepository.GetByIdAsync(id);

        if (park == null) return NotFound();

        return Ok(_mapper.Map<ParkDto>(park));
    }
}
