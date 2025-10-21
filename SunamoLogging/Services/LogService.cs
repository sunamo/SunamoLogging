// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Services;

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