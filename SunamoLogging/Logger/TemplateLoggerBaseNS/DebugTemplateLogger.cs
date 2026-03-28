namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

/// <summary>
/// Debug implementation of template logger that writes to console.
/// </summary>
public class DebugTemplateLogger : TemplateLoggerBase
{
    static DebugTemplateLogger? loggerInstance =
#if DEBUG2
    new DebugTemplateLogger();
#elif !DEBUG2
    null;
#endif

    /// <summary>
    /// Gets the singleton instance of the debug template logger.
    /// </summary>
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

    /// <summary>
    /// Writes a debug message to console.
    /// Cannot use DebugLogger.DebugWriteLine as it won't be available in release builds due to #if DEBUG.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message format string.</param>
    /// <param name="args">Format arguments.</param>
    static void DebugWriteLine(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        Console.WriteLine(string.Format(message, args));
    }
}