// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.TemplateLoggerBaseNS;

public class DummyTemplateLogger : TemplateLoggerBase
{
    public static DummyTemplateLogger Instance = new();

    private DummyTemplateLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {

    }


}
