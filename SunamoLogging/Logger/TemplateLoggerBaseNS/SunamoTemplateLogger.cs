namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class SunamoTemplateLogger : TemplateLoggerBase
{
    public static SunamoTemplateLogger Instance { get; set; } = new();

    private SunamoTemplateLogger() : base(ThisApp.SetStatus)
    {
    }
}
