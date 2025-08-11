using Microsoft.EntityFrameworkCore;

public class FootballLeagueDbContext : DbContext
{
    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
}