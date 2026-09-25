namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class DummyTemplateLogger : TemplateLoggerBase
{
    public static DummyTemplateLogger Instance { get; set; } = new();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {
    }
}
