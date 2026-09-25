namespace SunamoLogging.Logger.TypedLoggerBaseNS;

public class TypedDebugLogger : TypedLoggerBase
{
    public static TypedDebugLogger Instance { get; set; } = new();

    private TypedDebugLogger() : base(WriteLineWorker)
    {
    }

    public static void WriteLineWorker(TypeOfMessageLogging messageType, string message, params string[] args)
    {
        Console.WriteLine(string.Format(message, args));
    }
}
