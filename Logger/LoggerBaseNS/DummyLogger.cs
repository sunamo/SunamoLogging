namespace SunamoLogger.Logger.LoggerBaseNS;

public class DummyLogger : LoggerBase
{
    public static DummyLogger Instance = new DummyLogger();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {

    }




}
