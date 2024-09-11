
namespace SunamoLogging.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class LogService(ILogger logger)
{
    public void LogCriticalJson(dynamic d)
    {
        logger.LogCritical(JsonSerializer.Serialize(d as ExpandoObject));
    }

    public void LogDebugJson(dynamic d)
    {
        logger.LogDebug(JsonSerializer.Serialize(d as ExpandoObject));
    }

    public void LogErrorJson(dynamic d)
    {
        logger.LogError(JsonSerializer.Serialize(d as ExpandoObject));
    }

    public void LogInformationJson(dynamic d)
    {
        logger.LogInformation(JsonSerializer.Serialize(d as ExpandoObject));
    }

    public void LogTraceJson(dynamic d)
    {
        logger.LogTrace(JsonSerializer.Serialize(d as ExpandoObject));
    }

    public void LogWarningJson(dynamic d)
    {
        logger.LogWarning(JsonSerializer.Serialize(d as ExpandoObject));
    }


}