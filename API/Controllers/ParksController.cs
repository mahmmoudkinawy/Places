namespace API.Controllers;
public class ParksController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ParksController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ParkDto>>> GetAllParksAsync() =>
        Ok(_mapper.Map<IReadOnlyCollection<ParkDto>>(await _unitOfWork.ParkRepository.GetAllAsync()));

    [HttpGet("{id}", Name = "GetParkAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ParkDto>> GetParkAsync([FromRoute] int id)
    {
        var park = await _unitOfWork.ParkRepository.GetByIdAsync(p => p.Id == id);

        if (park == null) return NotFound();

        return Ok(_mapper.Map<ParkDto>(park));
    }

    //I will try to refactore it
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ParkDto>> CreateParkAsync([FromBody] CreateParkDto createParkDto)
    {
        if (createParkDto == null) return BadRequest();

        var createdPark = _mapper.Map<Park>(createParkDto);

        var parkFromRepo = await _unitOfWork.ParkRepository
            .GetByNameAsync(p => p.Name.ToLower().Contains(createdPark.Name.ToLower()));

        if (parkFromRepo != null)
            return BadRequest($"Park name: {createdPark.Name} already exists");

        _unitOfWork.ParkRepository.Add(createdPark);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetParkAsync), new
        {
            id = createdPark.Id
        }, createdPark);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateParkAsync([FromRoute] int id,
        [FromBody] UpdateParkDto updateParkDto)
    {
        if (updateParkDto == null || id != updateParkDto.Id)
            return BadRequest();

        _unitOfWork.ParkRepository.Update(_mapper.Map<Park>(updateParkDto));
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteParkAsync([FromRoute] int id)
    {
        var parkFromDb = await _unitOfWork.ParkRepository.GetByIdAsync(p => p.Id == id);

        if (parkFromDb == null) return NotFound();

        _unitOfWork.ParkRepository.Delete(parkFromDb);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

}
