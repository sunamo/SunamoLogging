namespace SunamoLogging._sunamo.SunamoShared.Helpers.Runtime;

internal class RuntimeHelper
{
    internal static void EmptyDummyMethod(string message, params Object[] args)
    {
        // parameters are required by the delegate signature (used in DummyLogger)
        _ = message;
        _ = args;
    }

    internal static void EmptyDummyMethod()
    {
    }

    internal static void EmptyDummyMethodLogMessageImpl(TypeOfMessageLogging messageType, string message, params Object[] args)
    {
        // parameters are required by the delegate signature (EmptyDummyMethodLogMessage)
        _ = messageType;
        _ = message;
        _ = args;
    }

    internal static Action<TypeOfMessageLogging, string, Object[]> EmptyDummyMethodLogMessage { get; set; } = EmptyDummyMethodLogMessageImpl;
}
