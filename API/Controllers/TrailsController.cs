﻿namespace API.Controllers;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TrailDto>>> GetAllTrailsWithParksAsync()
        => Ok(_mapper.Map<IReadOnlyCollection<TrailDto>>(await _trailRepository.GetAllAsync(includeProperties: "Park")));


    [HttpGet("{id}", Name = "GetTrailWithParkAsync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrailDto>> GetTrailWithParkAsync([FromRoute] int id)
    {
        var trail = await _trailRepository.GetByIdAsync(t => t.Id == id, includeProperties: "Park");

        if (trail == null) return NotFound();

        return Ok(_mapper.Map<TrailDto>(trail));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrailDto>> CreateTrailAsync([FromBody] TrailCreateDto trailCreateDto)
    {
        if (trailCreateDto == null) return BadRequest();

        var createdTrail = _mapper.Map<Trail>(trailCreateDto);

        var trailFromRepo = await _trailRepository
                .GetByNameAsync(t => t.Name.ToLower().Contains(createdTrail.Name.ToLower()));

        if (trailFromRepo != null)
            return BadRequest($"Trail with {createdTrail.Name} is already exists.");

        _trailRepository.Add(createdTrail);
        await _trailRepository.SaveChangesAsync();

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

        _trailRepository.Update(_mapper.Map<Trail>(updateTrailDto));
        await _trailRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrailAsync([FromRoute] int id)
    {
        var trail = await _trailRepository.GetByIdAsync(t => t.Id == id);

        if (trail == null) return NotFound();

        _trailRepository.Delete(trail);
        await _trailRepository.SaveChangesAsync();

        return NoContent();
    }

}
