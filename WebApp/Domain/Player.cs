public class Player
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Country { get; set; }
    public int Age { get; set; }
    public int TeamId { get; set; }
    public required Team Team { get; set; }
}
