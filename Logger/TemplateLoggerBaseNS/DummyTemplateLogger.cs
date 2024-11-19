namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class DummyTemplateLogger : TemplateLoggerBase
{
    public static DummyTemplateLogger Instance = new();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {

    }


}
