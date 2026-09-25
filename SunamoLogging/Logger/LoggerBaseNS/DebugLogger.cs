namespace SunamoLogging.Logger.LoggerBaseNS;

/// <summary>
/// Debug logger implementation that writes to debug output.
/// This logger is available in Sunamo and provides debug output functionality.
/// For faster logging without explicit methods, create a custom logger class in your application project.
/// The compiler should optimize the calls to be equally efficient.
/// </summary>
public class DebugLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    /// <summary>
    /// Gets the singleton instance of the debug logger.
    /// </summary>
    public static LoggerBase Instance
    {
        get
        {
            if (LoggerInstance == null)
            {
                throw new Exception("Dont use DebugLogger without #if DEBUG!!");
            }
            return LoggerInstance;
        }
    }

    /// <summary>
    /// Breaks into debugger in DEBUG mode.
    /// </summary>
    public static void BreakOrReadLine()
    {
    }

    /// <summary>
    /// Singleton instance of the debug logger.
    /// MUST be always in #if DEBUG - otherwise throws anonymous error in release and it's hard to find!
    /// </summary>
    public static DebugLogger? LoggerInstance { get; set; }

    /// <summary>
    /// Writes a formatted debug message with message type prefix.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message format string.</param>
    /// <param name="args">Format arguments.</param>
    /// <summary>
    /// Writes a formatted debug message.
    /// </summary>
    /// <param name="message">The message format string.</param>
    /// <param name="args">Format arguments.</param>

    /// <summary>
    /// Breaks into the debugger.
    /// </summary>
    public static void Break()
    {
        System.Diagnostics.Debugger.Break();
    }
}