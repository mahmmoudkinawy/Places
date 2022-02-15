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

    [HttpGet("{id}", Name = "GetParkAsync")]
    public async Task<ActionResult<ParkDto>> GetParkAsync([FromRoute] int id)
    {
        var park = await _parkRepository.GetByIdAsync(id);

        if (park == null) return NotFound();

        return Ok(_mapper.Map<ParkDto>(park));
    }

    [HttpPost]
    //I will try to refactore it
    public async Task<IActionResult> CreateParkAsync([FromBody] CreateParkDto createParkDto)
    {
        if (createParkDto == null) return BadRequest();

        var createdPark = _mapper.Map<Park>(createParkDto);

        var parkFromRepo = await _parkRepository
            .GetByNameAsync(p => p.Name.ToLower().Contains(createParkDto.Name.ToLower()));

        if (parkFromRepo != null)
            return BadRequest($"Park name: {createdPark.Name} already exists");

        _parkRepository.Add(createdPark);
        await _parkRepository.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetParkAsync), new
        {
            id = createdPark.Id
        }, createdPark);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateParkAsync([FromRoute] int id,
        [FromBody] UpdateParkDto updateParkDto)
    {
        if (updateParkDto == null || id != updateParkDto.Id)
            return BadRequest();

        _parkRepository.Update(_mapper.Map<Park>(updateParkDto));
        await _parkRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParkAsync([FromRoute] int id)
    {
        var parkFromDb = await _parkRepository.GetByIdAsync(id);

        if (parkFromDb == null) return NotFound();

        _parkRepository.Delete(parkFromDb);
        await _parkRepository.SaveChangesAsync();

        return NoContent();
    }

}
