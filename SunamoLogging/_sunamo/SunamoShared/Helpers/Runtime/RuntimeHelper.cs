namespace SunamoLogging._sunamo.SunamoShared.Helpers.Runtime;

/// <summary>
/// Empty methods used as default delegates of the dummy loggers.
/// </summary>
internal class RuntimeHelper
{
    /// <summary>
    /// Does nothing; placeholder for a write-line delegate.
    /// </summary>
    internal static void EmptyDummyMethod(string message, params object[] args)
    {
    }

    /// <summary>
    /// Does nothing; placeholder for a typed write-line delegate.
    /// </summary>
    internal static void EmptyDummyMethodLogMessage(TypeOfMessageLogging messageType, string message, params object[] args)
    {
    }
}
