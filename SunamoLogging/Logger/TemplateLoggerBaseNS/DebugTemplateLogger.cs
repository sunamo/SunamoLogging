// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.TemplateLoggerBaseNS;
public class DebugTemplateLogger : TemplateLoggerBase
{
    public static Type type = typeof(DebugTemplateLogger);
    static DebugTemplateLogger instance =
#if DEBUG2
    new DebugTemplateLogger();
#elif !DEBUG2
    //new DebugLogger(DebugWriteLine);
    null;
#endif
    public static TemplateLoggerBase Instance
    {
        get
        {
            if (instance == null)
            {
                throw new Exception("Dont use DebugLogger without #if DEBUG!!");
                return DummyTemplateLogger.Instance;
            }
            return instance;
        }
    }
    private DebugTemplateLogger() : base(DebugWriteLine)
    {
    }
    /// <summary>
    /// Nemůžu použít DebugLogger.DebugWriteLine - do release balíčku ho nedostanu přes #if DEBUG
    /// </summary>
    /// <param name="t"></param>
    /// <param name="m"></param>
    /// <param name="args"></param>
    static void DebugWriteLine(TypeOfMessageLogging t, string m, params string[] args)
    {
        // todo vykreslit barevně
        Console.WriteLine(string.Format(m, args));
    }
}