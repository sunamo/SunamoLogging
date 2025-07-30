namespace SunamoLogging._sunamo.SunamoShared.Helpers.Runtime;



internal class RuntimeHelper
{
#pragma warning disable
    internal static void EmptyDummyMethod(string s, params Object[] o)
    {
    }
#pragma warning restore

    internal static Action<TypeOfMessageLogging, string, Object[]> EmptyDummyMethodLogMessage;
}
