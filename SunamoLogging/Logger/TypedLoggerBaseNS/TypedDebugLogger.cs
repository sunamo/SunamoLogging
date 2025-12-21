namespace SunamoLogging.Logger.TypedLoggerBaseNS;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public class TypedDebugLogger : TypedLoggerBase
{
#if DEBUG //2
    public static TypedDebugLogger Instance = new();
    private TypedDebugLogger() : base(DebugLogger.DebugWriteLine)
    {
    }
#endif
}
//#endif