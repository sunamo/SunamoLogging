namespace SunamoLogging.Logger.TypedLoggerBaseNS;

public class TypedDummyLogger : TypedLoggerBase
{
    public static TypedDummyLogger Instance { get; set; } = new();

    private TypedDummyLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {
    }
}
