namespace SunamoLogging.Logger.TypedLoggerBaseNS;

public class TypedSunamoLogger : TypedLoggerBase
{
    public static TypedSunamoLogger Instance { get; set; } = new();

    private TypedSunamoLogger() : base(WriteLineWorker)
    {
    }

    public static void WriteLineWorker(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        ThisApp.SetStatus(messageType, message, args);
    }
}
