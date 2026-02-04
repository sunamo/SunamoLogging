namespace SunamoLogging.Services;

/// <summary>
/// Logging service that serializes dynamic objects to JSON before logging.
/// </summary>
public class LogService(ILogger logger)
{
    /// <summary>
    /// Logs a critical message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogCriticalJson(dynamic data)
    {
        logger.LogCritical(JsonSerializer.Serialize(data as ExpandoObject));
    }

    /// <summary>
    /// Logs a debug message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogDebugJson(dynamic data)
    {
        logger.LogDebug(JsonSerializer.Serialize(data as ExpandoObject));
    }

    /// <summary>
    /// Logs an error message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogErrorJson(dynamic data)
    {
        logger.LogError(JsonSerializer.Serialize(data as ExpandoObject));
    }

    /// <summary>
    /// Logs an information message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogInformationJson(dynamic data)
    {
        logger.LogInformation(JsonSerializer.Serialize(data as ExpandoObject));
    }

    /// <summary>
    /// Logs a trace message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogTraceJson(dynamic data)
    {
        logger.LogTrace(JsonSerializer.Serialize(data as ExpandoObject));
    }

    /// <summary>
    /// Logs a warning message with JSON serialized object.
    /// </summary>
    /// <param name="data">The dynamic object to serialize and log.</param>
    public void LogWarningJson(dynamic data)
    {
        logger.LogWarning(JsonSerializer.Serialize(data as ExpandoObject));
    }
}