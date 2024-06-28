namespace SunamoLogging;

public class SunamoTemplateLogger : TemplateLoggerBase
{
    public static SunamoTemplateLogger Instance = new SunamoTemplateLogger();

    private SunamoTemplateLogger() : base(ThisApp.SetStatus)
    {
    }




}
