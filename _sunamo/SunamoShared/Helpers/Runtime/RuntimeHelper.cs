namespace SunamoLogging._sunamo.SunamoShared.Helpers.Runtime;



internal class RuntimeHelper
{

    internal static void EmptyDummyMethod(string s, params Object[] o)
    {
    }

    internal static Action<TypeOfMessageLogging, string, Object[]> EmptyDummyMethodLogMessage;
}
