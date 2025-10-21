using Microsoft.EntityFrameworkCore;

public class FootballLeagueDbContext: DbContext
{
    public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
    {
    }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
}