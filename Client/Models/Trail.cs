namespace Client.Models;
public class Trail
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Distance { get; set; }

    public DateTime DateCreated { get; set; }

    public DifficultyType Difficulty { get; set; }

    [Required] 
    public int ParkId { get; set; }

    public Park Park { get; set; }
}