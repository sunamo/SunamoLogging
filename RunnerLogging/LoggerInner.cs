namespace RunnerLogging;

using Microsoft.Extensions.Logging;

/// <summary>
/// Inner logger that demonstrates direct ILogger usage with various log levels.
/// </summary>
/// <param name="logger">The Microsoft.Extensions.Logging logger instance.</param>
internal class LoggerInner(ILogger logger)
{
    /// <summary>
    /// Logs test messages at all log levels.
    /// </summary>
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
