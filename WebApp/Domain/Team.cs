public class Team
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public int YearFounded { get; set; }
    public int LeagueId { get; set; }
    public required League League { get; set; }  
}
