namespace SunamoLogging.Logger.LoggerBaseNS;

public class DebugLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    public static LoggerBase Instance
    {
        get
        {
            if (LoggerInstance == null)
            {
                throw new Exception("Dont use DebugLogger without #if DEBUG!!");
            }
            return LoggerInstance;
        }
    }

    public static void BreakOrReadLine()
    {
    }

    // MUST be always in #if DEBUG - otherwise throws anonymous error in release and it's hard to find!
    public static DebugLogger? LoggerInstance { get; set; }


    public static void Break()
    {
        System.Diagnostics.Debugger.Break();
    }
}
