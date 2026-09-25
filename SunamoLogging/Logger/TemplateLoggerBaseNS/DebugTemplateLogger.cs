namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class DebugTemplateLogger : TemplateLoggerBase
{
    static DebugTemplateLogger? loggerInstance = new();

    public static TemplateLoggerBase Instance
    {
        get
        {
            if (loggerInstance == null)
            {
                throw new Exception("Dont use DebugLogger without #if DEBUG!!");
            }
            return loggerInstance;
        }
    }

    private DebugTemplateLogger() : base(DebugWriteLine)
    {
    }

    // Cannot use DebugLogger.DebugWriteLine as it won't be available in release builds due to #if DEBUG.
    static void DebugWriteLine(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        Console.WriteLine(string.Format(message, args));
    }
}
