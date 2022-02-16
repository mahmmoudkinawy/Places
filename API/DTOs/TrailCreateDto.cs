namespace API.DTOs;
public class TrailCreateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double Distance { get; set; }

    //public DateTime DateCreated { get; set; }

    public DifficultyType Difficulty { get; set; }

    [Required]
    public int ParkId { get; set; }
}
