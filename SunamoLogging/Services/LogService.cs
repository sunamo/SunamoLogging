namespace SunamoLogging.Services;

public class LogService(ILogger logger)
{
    public void LogCriticalJson(dynamic data)
    {
        logger.LogCritical(JsonSerializer.Serialize(data as ExpandoObject));
    }

    public void LogDebugJson(dynamic data)
    {
        logger.LogDebug(JsonSerializer.Serialize(data as ExpandoObject));
    }

    public void LogErrorJson(dynamic data)
    {
        logger.LogError(JsonSerializer.Serialize(data as ExpandoObject));
    }

    public void LogInformationJson(dynamic data)
    {
        logger.LogInformation(JsonSerializer.Serialize(data as ExpandoObject));
    }

    public void LogTraceJson(dynamic data)
    {
        logger.LogTrace(JsonSerializer.Serialize(data as ExpandoObject));
    }

    public void LogWarningJson(dynamic data)
    {
        logger.LogWarning(JsonSerializer.Serialize(data as ExpandoObject));
    }
}
