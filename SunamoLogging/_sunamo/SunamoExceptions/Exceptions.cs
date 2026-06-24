namespace SunamoLogging._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    internal static string TextOfExceptions(Exception exception, bool includeInnerExceptions = true)
    {
        if (exception == null) return string.Empty;
        StringBuilder stringBuilder = new();
        stringBuilder.Append("Exception:");
        stringBuilder.AppendLine(exception.Message);
        if (includeInnerExceptions)
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                stringBuilder.AppendLine(exception.Message);
            }
        var result = stringBuilder.ToString();
        return result;
    }

    internal static Tuple<string, string, string> PlaceOfException(bool extractTypeAndMethod = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);

        string typeName = string.Empty;
        string methodName = string.Empty;
        for (int index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            if (extractTypeAndMethod)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out typeName, out methodName);
                    extractTypeAndMethod = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(typeName, methodName, string.Join(Environment.NewLine, lines));
    }

    internal static void TypeAndMethodName(string stackTraceLine, out string typeName, out string methodName)
    {
        var trimmedLine = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = trimmedLine.Split("(")[0];
        var pathParts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathParts[^1];
        pathParts.RemoveAt(pathParts.Count - 1);
        typeName = string.Join(".", pathParts);
    }

    internal static string CallingMethod(int frameDepth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameDepth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal static string? IsNullOrWhitespace(string prefix, string argumentName, string argumentValue, bool disallowWhitespaceOnly)
    {
        string additionalParams;
        if (argumentValue == null)
        {
            additionalParams = AddParams();
            return CheckBefore(prefix) + argumentName + " is null" + additionalParams;
        }
        if (argumentValue == string.Empty)
        {
            additionalParams = AddParams();
            return CheckBefore(prefix) + argumentName + " is empty (without trim)" + additionalParams;
        }
        if (disallowWhitespaceOnly && argumentValue.Trim() == string.Empty)
        {
            additionalParams = AddParams();
            return CheckBefore(prefix) + argumentName + " is empty (with trim)" + additionalParams;
        }
        return null;
    }

    readonly static StringBuilder additionalInfoInnerStringBuilder = new();
    readonly static StringBuilder additionalInfoStringBuilder = new();

    internal static string AddParams()
    {
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoStringBuilder.Insert(0, "Outer:");
        additionalInfoStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        additionalInfoInnerStringBuilder.Insert(0, "Inner:");
        additionalInfoInnerStringBuilder.Insert(0, Environment.NewLine);
        var outerParams = additionalInfoStringBuilder.ToString();
        var innerParams = additionalInfoInnerStringBuilder.ToString();
        return outerParams + innerParams;
    }
    #endregion

    #region OnlyReturnString
    internal static string? AnyElementIsNullOrEmpty(string prefix, string collectionName, IEnumerable<int> nullIndexes)
    {
        return CheckBefore(prefix) + $"In {collectionName} has indexes " + string.Join(",", nullIndexes) +
        " with null value";
    }

    internal static string? NotEvenNumberOfElements(string prefix, string collectionName)
    {
        return CheckBefore(prefix) + collectionName + " have odd elements count";
    }

    internal static string? Custom(string prefix, string message)
    {
        return CheckBefore(prefix) + message;
    }
    #endregion

    internal static string? NotImplementedCase(string prefix, object notImplementedValue)
    {
        var forClause = string.Empty;
        if (notImplementedValue != null)
        {
            forClause = " for ";
            if (notImplementedValue.GetType() == typeof(Type))
                forClause += ((Type)notImplementedValue).FullName;
            else
                forClause += notImplementedValue.ToString();
        }
        return CheckBefore(prefix) + "Not implemented case" + forClause + " . internal program error. Please contact developer" +
        ".";
    }
}
