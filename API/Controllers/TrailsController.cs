namespace API.Controllers;
public class TrailsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TrailsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrailDto>>> GetAllTrailsWithParksAsync()
        => Ok(_mapper.Map<IReadOnlyCollection<TrailDto>>(await _unitOfWork.TrailRepository.GetAllAsync(includeProperties: "Park")));


    [HttpGet("{id}", Name = "GetTrailWithParkAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrailDto>> GetTrailWithParkAsync([FromRoute] int id)
    {
        var trail = await _unitOfWork.TrailRepository.GetByIdAsync(t => t.Id == id, includeProperties: "Park");

        if (trail == null) return NotFound();

        return Ok(_mapper.Map<TrailDto>(trail));
    }


    [HttpGet("get-trail-in-park/{parkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<TrailDto>>> GetTrailsInParkAsync
        ([FromRoute] int parkId)
    {
        var trailsInPark = await _unitOfWork.TrailRepository.GetTrailsInParkAsync(parkId);

        if (trailsInPark == null) return NotFound();

        return Ok(_mapper.Map<IReadOnlyCollection<TrailDto>>(trailsInPark));
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrailDto>> CreateTrailAsync([FromBody] TrailCreateDto trailCreateDto)
    {
        if (trailCreateDto == null) return BadRequest();

        var createdTrail = _mapper.Map<Trail>(trailCreateDto);

        var trailFromRepo = await _unitOfWork.TrailRepository
                .GetByNameAsync(t => t.Name.ToLower().Contains(createdTrail.Name.ToLower()));

        if (trailFromRepo != null)
            return BadRequest($"Trail with {createdTrail.Name} is already exists.");

        _unitOfWork.TrailRepository.Add(createdTrail);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtRoute(nameof(GetTrailWithParkAsync), new
        {
            id = createdTrail.Id
        }, createdTrail);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTrailAsync([FromRoute] int id,
        [FromBody] UpdateTrailDto updateTrailDto)
    {
        if (updateTrailDto == null || id != updateTrailDto.Id) return BadRequest();

        _unitOfWork.TrailRepository.Update(_mapper.Map<Trail>(updateTrailDto));
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrailAsync([FromRoute] int id)
    {
        var trail = await _unitOfWork.TrailRepository.GetByIdAsync(t => t.Id == id);

        if (trail == null) return NotFound();

        _unitOfWork.TrailRepository.Delete(trail);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

}
