namespace API.Entities;
public class Trail
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Distance { get; set; }

    public DateTime DateCreated { get; set; }

    public DifficultyType Difficulty { get; set; }

    [Required] //Foregin key
    public int ParkId { get; set; }

    [ForeignKey("ParkId")]
    public Park Park { get; set; }
}
