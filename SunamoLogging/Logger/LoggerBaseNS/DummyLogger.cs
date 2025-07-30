namespace SunamoLogging.Logger.LoggerBaseNS;

public class DummyLogger : LoggerBase
{
    public static DummyLogger Instance = new();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }




}
