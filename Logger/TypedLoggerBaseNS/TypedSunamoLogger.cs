namespace SunamoLogging.Logger.TypedLoggerBaseNS;



public class TypedSunamoLogger : TypedLoggerBase
{
    public static TypedSunamoLogger Instance = new();

    private TypedSunamoLogger() : base(WriteLineWorker)
    {
    }

    public static void WriteLineWorker(TypeOfMessageLogging tz, string text, params string[] args)
    {
        ThisApp.SetStatus(tz, text, args);
    }


}
