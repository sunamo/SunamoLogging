namespace SunamoLogging.Logger.TypedLoggerBaseNS;

/// <summary>
/// Debug typed logger implementation that writes to debug output.
/// </summary>
public class TypedDebugLogger : TypedLoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the typed debug logger.
    /// </summary>
    public static TypedDebugLogger Instance { get; set; } = new();

    private TypedDebugLogger() : base(WriteLineWorker)
    {
    }

    public static void WriteLineWorker(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        Console.WriteLine(string.Format(message, args));
    }
}