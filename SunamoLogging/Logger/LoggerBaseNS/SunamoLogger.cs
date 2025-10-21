// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoLogging.Logger.LoggerBaseNS;


public class SunamoLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    public static SunamoLogger Instance { get; set; } = new(WriteLineWorker);

    public static void WriteLineWorker(string text, params string[] args)
    {
        ThisApp.Ordinal(text, args);
    }


}
