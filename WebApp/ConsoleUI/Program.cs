using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;

var config = BuildConfig();
var connectionString = BuildConnectionString(config);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<FootballLeagueDbContext>(options => options.UseNpgsql(connectionString));
    })
    .Build();

var svc = ActivatorUtilities.CreateInstance<ApplicationService>(host.Services);

try
{
    svc.Start();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application failed to start.");
    svc.Stop();
    Log.CloseAndFlush();
}

static IConfigurationRoot BuildConfig()
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>();

    return builder.Build();
}

static string? BuildConnectionString(IConfigurationRoot config)
{
    var connectionBuilder = new NpgsqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
    connectionBuilder.Username = config["database:username"];
    connectionBuilder.Password = config["database:password"];

    return connectionBuilder.ConnectionString;
}
