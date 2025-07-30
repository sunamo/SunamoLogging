namespace RunnerLogging;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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