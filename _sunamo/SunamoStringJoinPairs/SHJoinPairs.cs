namespace SunamoLogging._sunamo.SunamoStringJoinPairs;

internal class SHJoinPairs
{
    //internal static Func<string[], string> JoinPairs;

    internal static string JoinPairs(string firstDelimiter, string secondDelimiter, params string[] parts)
    {
        //InitApp.TemplateLogger.NotEvenNumberOfElements(type, "JoinPairs", @"args", parts);
        //InitApp.TemplateLogger.AnyElementIsNull(type, "JoinPairs", @"args", parts);

        StringBuilder sb = new();
        for (int i = 0; i < parts.Length; i++)
        {
            sb.Append(parts[i++] + firstDelimiter);
            sb.Append(parts[i] + secondDelimiter);
        }
        return sb.ToString();
    }
}
