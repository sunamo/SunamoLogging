namespace SunamoLogging.Logger.TypedLoggerBaseNS;

/// <summary>
/// Dummy typed logger implementation that does nothing.
/// Used as a null object pattern for when typed logging is disabled.
/// </summary>
public class TypedDummyLogger : TypedLoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the typed dummy logger.
    /// </summary>
    public static TypedDummyLogger Instance = new();

    private TypedDummyLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {
    }
}