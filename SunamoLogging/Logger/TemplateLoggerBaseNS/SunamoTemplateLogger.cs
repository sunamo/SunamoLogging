namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

/// <summary>
/// Sunamo template logger implementation that uses ThisApp.SetStatus for output.
/// </summary>
public class SunamoTemplateLogger : TemplateLoggerBase
{
    /// <summary>
    /// Gets the singleton instance of the Sunamo template logger.
    /// </summary>
    public static SunamoTemplateLogger Instance = new();

    private SunamoTemplateLogger() : base(ThisApp.SetStatus)
    {
    }
}