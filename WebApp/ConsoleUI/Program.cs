using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices((context, services) =>
    {
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

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}
