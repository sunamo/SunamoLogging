// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.LoggerBaseNS;

public class DummyLogger : LoggerBase
{
    public static DummyLogger Instance = new();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }




}
