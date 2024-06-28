namespace SunamoLogging;



internal class RuntimeHelper
{
    internal static void EmptyDummyMethod()
    {
    }

    internal static void EmptyDummyMethod(string s, params Object[] o)
    {
    }

    internal static Action<TypeOfMessage, string, Object[]> EmptyDummyMethodLogMessage;
}
