namespace SunamoLogging;



public class RuntimeHelper
{
    public static void EmptyDummyMethod()
    {
    }

    public static void EmptyDummyMethod(string s, params Object[] o)
    {
    }

    public static Action<TypeOfMessage, string, Object[]> EmptyDummyMethodLogMessage;
}
