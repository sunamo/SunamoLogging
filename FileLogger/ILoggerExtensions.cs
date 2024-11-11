namespace SunamoLogging.FileLogger;
public static class ILoggerExtensions
{
    private static void LogInformationJson(this ILogger logger, dynamic d)
    {
        //logger.LogInformation(JsonSerializer.Serialize(d));
    }
}