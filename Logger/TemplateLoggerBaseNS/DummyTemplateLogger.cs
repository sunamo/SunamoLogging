namespace SunamoLogging;

public class DummyTemplateLogger : TemplateLoggerBase
{
    public static DummyTemplateLogger Instance = new DummyTemplateLogger();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {

    }


}
