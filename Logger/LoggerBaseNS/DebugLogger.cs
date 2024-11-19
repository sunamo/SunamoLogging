namespace SunamoLogging.Logger.LoggerBaseNS;

/// <summary>
/// Tento //////DebugLogger.Instance je ve sunamo, obsahuje jedinou metodu, kterou používej ve //////DebugLogger.Instance např. apps
/// Pokud chceš rychleji zapisovat a nepotřebuješ explicitně nějaké metody, vytvoř si vlastní třídu //////DebugLogger.Instance v projektu aplikace. Ono by s_tejně kompilátor měl poznat že jen volá něco jiného a tak by to mělo být stejně efektivní
/// </summary>
public class DebugLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    public static Type type = typeof(DebugLogger);

    public static LoggerBase Instance
    {
        get
        {
            if (instance == null)
            {
                throw new Exception("Dont use DebugLogger without #if DEBUG!!");
                return DummyLogger.Instance;
            }
            return instance;
        }
    }

    public static void BreakOrReadLine()
    {
#if DEBUG
        System.Diagnostics.Debugger.Break();
#elif !DEBUG
//CL.ReadLine();
#endif
    }



    /// <summary>
    /// Must be always in #if debug - otherwise throw anonymous error in release and its hard to find it!!!
    /// </summary>
    public static DebugLogger instance = null;
#if DEBUG //2
    public static void DebugWriteLine(TypeOfMessageLogging tz, string text, params Object[] args)
    {
        //DebugLogger.DebugWriteLine(tz.ToString() + ":" + string.Format(text, args));
    }

    public static void DebugWriteLine(string text, params string[] args)
    {
        Debug.WriteLine(string.Format(text, args));
    }
#endif

    public static void Break()
    {
        System.Diagnostics.Debugger.Break();
    }


}
