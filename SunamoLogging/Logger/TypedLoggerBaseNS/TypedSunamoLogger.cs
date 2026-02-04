namespace SunamoLogging.Logger.TypedLoggerBaseNS;

/// <summary>
/// Sunamo typed logger implementation that uses ThisApp.SetStatus for output.
/// </summary>
public class TypedSunamoLogger : TypedLoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the typed Sunamo logger.
    /// </summary>
    public static TypedSunamoLogger Instance = new();

    private TypedSunamoLogger() : base(WriteLineWorker)
    {
    }

    /// <summary>
    /// Worker method that writes log messages via ThisApp.SetStatus.
    /// </summary>
    /// <param name="messageType">The type of the message.</param>
    /// <param name="message">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public static void WriteLineWorker(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        ThisApp.SetStatus(messageType, message, args);
    }
}