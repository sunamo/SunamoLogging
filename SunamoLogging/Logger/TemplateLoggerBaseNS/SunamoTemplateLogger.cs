// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class SunamoTemplateLogger : TemplateLoggerBase
{
    public static SunamoTemplateLogger Instance = new();

    private SunamoTemplateLogger() : base(ThisApp.SetStatus)
    {
    }




}
