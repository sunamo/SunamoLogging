namespace SunamoLogging.Logger.LoggerBaseNS;

public class SunamoLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    public static SunamoLogger Instance { get; set; } = new(WriteLineWorker);

    public static void WriteLineWorker(string text, params string[] args)
    {
        ThisApp.Ordinal(text, args);
    }


}