namespace SunamoLogging.Logger.TypedLoggerBaseNS;


public class TypedDummyLogger : TypedLoggerBase
{
    public static TypedDummyLogger Instance = new();

    private TypedDummyLogger() : base(RuntimeHelper.EmptyDummyMethodLogMessage)
    {

    }


}
