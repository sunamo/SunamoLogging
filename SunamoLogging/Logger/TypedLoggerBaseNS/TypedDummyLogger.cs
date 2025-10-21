// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.TypedLoggerBaseNS;


public class TypedDummyLogger : TypedLoggerBase
{
    public static TypedDummyLogger Instance = new();

    private TypedDummyLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {

    }


}
