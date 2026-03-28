namespace SunamoLogging.Logger.TypedLoggerBaseNS;

/// <summary>
/// Debug typed logger implementation that writes to debug output.
/// </summary>
public class TypedDebugLogger : TypedLoggerBase
{
#if DEBUG
    /// <summary>
    /// Gets the singleton instance of the typed debug logger.
    /// </summary>
    public static TypedDebugLogger Instance { get; set; } = new();

    private TypedDebugLogger() : base(DebugLogger.DebugWriteLine)
    {
    }
#endif
}