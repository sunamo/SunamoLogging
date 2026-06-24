namespace RunnerLogging;

using Microsoft.Extensions.Logging;

internal class LoggerInner(ILogger logger)
{
    public void Log()
    {
        logger.LogCritical("Critical!");
        logger.LogError("Error!");
        logger.LogWarning("Warning!");
        logger.LogInformation("Info!");
        logger.LogTrace("Trace!");
        logger.LogDebug("Debug!");
    }
}
