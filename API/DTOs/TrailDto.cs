namespace API.DTOs;
public class TrailDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double Distance { get; set; }

    public DifficultyType Difficulty { get; set; }

    public int ParkId { get; set; }

    public ParkDto Park { get; set; }
}
