namespace SunamoLogging.Logger.LoggerBaseNS;

/// <summary>
/// Sunamo logger implementation that uses ThisApp.Ordinal for output.
/// </summary>
public class SunamoLogger(Action<string, string[]> writeLineHandler) : LoggerBase(writeLineHandler)
{
    /// <summary>
    /// Gets or sets the singleton instance of the Sunamo logger.
    /// </summary>
    public static SunamoLogger Instance { get; set; } = new(WriteLineWorker);

    /// <summary>
    /// Worker method that writes log messages via ThisApp.Ordinal.
    /// </summary>
    /// <param name="text">The message text.</param>
    /// <param name="args">Format arguments.</param>
    public static void WriteLineWorker(string text, params string[] args)
    {
        ThisApp.Ordinal(text, args);
    }
}