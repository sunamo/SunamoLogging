namespace SunamoLogging.Logger.LoggerBaseNS;

/// <summary>
/// Dummy logger implementation that does nothing.
/// Used as a null object pattern for when logging is disabled.
/// </summary>
public class DummyLogger : LoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the dummy logger.
    /// </summary>
    public static DummyLogger Instance = new();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {
    }
}