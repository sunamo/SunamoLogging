namespace SunamoLogger;


public class SunamoLogger : LoggerBase
{
    public static SunamoLogger Instance = new SunamoLogger(WriteLineWorker);

    private SunamoLogger()
    {
        // Here it must be without Instance =, otherwise write Instance again with writeLineHandler=null
        //new SunamoLogger(WriteLine);
    }

    public SunamoLogger(Action<string, string[]> writeLineHandler) : base(writeLineHandler)
    {
    }

    public static void WriteLineWorker(string text, params string[] args)
    {
        ThisApp.Ordinal(text, args);
    }


}
