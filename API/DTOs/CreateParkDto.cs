namespace API.DTOs;
public class CreateParkDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string State { get; set; }
    public DateTime Created { get; set; }
    public DateTime Established { get; set; }
}
