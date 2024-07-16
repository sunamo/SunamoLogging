
namespace SunamoLogging.Logger.TypedLoggerBaseNS;
using SunamoLogging.Logger.LoggerBaseNS;

public class TypedDebugLogger : TypedLoggerBase
{
#if DEBUG //2
    public static TypedDebugLogger Instance = new TypedDebugLogger();

    private TypedDebugLogger() : base(DebugLogger.DebugWriteLine)
    {
    }




#endif
}
//#endif