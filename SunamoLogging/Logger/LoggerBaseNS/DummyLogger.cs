namespace SunamoLogging.Logger.LoggerBaseNS;

public class DummyLogger : LoggerBase
{
    public static DummyLogger Instance { get; set; } = new();

    private DummyLogger() : base(RuntimeHelper.EmptyDummyMethod)
    {
    }
}
