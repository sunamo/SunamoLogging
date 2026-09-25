namespace SunamoLogging;

public class InitApp
{
    // Alternatives are:
    //     InitApp.SetDebugLogger
    //     CmdApp.SetLogger
    //     WpfApp.SetLogger
    public static void SetDebugLogger()
    {
        TemplateLogger = DebugTemplateLogger.Instance;
        TypedLogger = TypedDebugLogger.Instance;
    }

    #region Must be set during app initializing
    public static ILoggerBase? Logger { get; set; }

    public static TypedLoggerBase? TypedLogger { get; set; }

    public static TemplateLoggerBase? TemplateLogger { get; set; }

    #endregion
}
