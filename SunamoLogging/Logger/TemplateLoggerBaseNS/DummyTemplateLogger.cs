namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

/// <summary>
/// Dummy template logger implementation that does nothing.
/// Used as a null object pattern for when template logging is disabled.
/// </summary>
public class DummyTemplateLogger : TemplateLoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the dummy template logger.
    /// </summary>
    public static DummyTemplateLogger Instance = new();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {
    }
}