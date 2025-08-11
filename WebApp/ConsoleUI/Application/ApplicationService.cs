
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

public class ApplicationService : IApplicationService
{
    private readonly ILogger<ApplicationService> _log;
    private readonly IConfiguration _config;

    public ApplicationService(ILogger<ApplicationService> log, IConfiguration config)
    {
        _log = log;
        _config = config;
    }
    public void Start()
    {
        _log.LogInformation("Application Service Started");
        // Add your service logic here
        var testValue = _config["test"];
        _log.LogInformation("Test config value: {TestValue}", testValue);
    }
    public void Stop()
    {
        _log.LogInformation("Application Service Stopped");
        // Add your cleanup logic here
    }
}