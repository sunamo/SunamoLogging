namespace SunamoLogging._sunamo.SunamoExceptions;

/// <summary>
/// Exception message formatting and analysis helper class.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Adds a prefix to a message if the prefix is not empty.
    /// </summary>
    /// <param name="prefix">The prefix to add.</param>
    /// <returns>The prefix with ": " suffix if not empty, otherwise empty string.</returns>
    internal static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }

    /// <summary>
    /// Extracts all exception messages including inner exceptions.
    /// </summary>
    /// <param name="exception">The exception to extract messages from.</param>
    /// <param name="includeInnerExceptions">Whether to include inner exception messages.</param>
    /// <returns>Formatted string containing all exception messages.</returns>
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

    /// <summary>
    /// Analyzes the stack trace to extract type, method name, and full stack trace information.
    /// </summary>
    /// <param name="extractTypeAndMethod">Whether to extract type and method from the first non-ThrowEx frame.</param>
    /// <returns>Tuple containing type name, method name, and formatted stack trace.</returns>
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

    /// <summary>
    /// Parses a stack trace line to extract type and method names.
    /// </summary>
    /// <param name="stackTraceLine">The stack trace line to parse.</param>
    /// <param name="typeName">Output parameter for the type name.</param>
    /// <param name="methodName">Output parameter for the method name.</param>
    internal static void TypeAndMethodName(string stackTraceLine, out string typeName, out string methodName)
    {
        var trimmedLine = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = trimmedLine.Split("(")[0];
        var pathParts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = pathParts[^1];
        pathParts.RemoveAt(pathParts.Count - 1);
        typeName = string.Join(".", pathParts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack frame depth.
    /// </summary>
    /// <param name="frameDepth">The stack frame depth (default is 1 for immediate caller).</param>
    /// <returns>The name of the calling method, or an error message if it cannot be retrieved.</returns>
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
    /// <summary>
    /// Checks if a string argument is null, empty, or whitespace and returns an error message if so.
    /// </summary>
    /// <param name="prefix">Prefix for the error message (typically the method name).</param>
    /// <param name="argumentName">The name of the argument being checked.</param>
    /// <param name="argumentValue">The value of the argument to check.</param>
    /// <param name="disallowWhitespaceOnly">Whether to disallow values containing only whitespace.</param>
    /// <returns>Error message if validation fails, null otherwise.</returns>
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

    readonly static StringBuilder sbAdditionalInfoInner = new();
    readonly static StringBuilder sbAdditionalInfo = new();

    /// <summary>
    /// Builds additional parameter information string.
    /// </summary>
    /// <returns>Formatted additional parameters string.</returns>
    internal static string AddParams()
    {
        sbAdditionalInfo.Insert(0, Environment.NewLine);
        sbAdditionalInfo.Insert(0, "Outer:");
        sbAdditionalInfo.Insert(0, Environment.NewLine);
        sbAdditionalInfoInner.Insert(0, Environment.NewLine);
        sbAdditionalInfoInner.Insert(0, "Inner:");
        sbAdditionalInfoInner.Insert(0, Environment.NewLine);
        var outerParams = sbAdditionalInfo.ToString();
        var innerParams = sbAdditionalInfoInner.ToString();
        return outerParams + innerParams;
    }
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// Creates an error message indicating that a collection contains null or empty elements at specific indexes.
    /// </summary>
    /// <param name="prefix">Prefix for the error message (typically the method name).</param>
    /// <param name="collectionName">The name of the collection being checked.</param>
    /// <param name="nullIndexes">The indexes where null or empty values were found.</param>
    /// <returns>Formatted error message.</returns>
    internal static string? AnyElementIsNullOrEmpty(string prefix, string collectionName, IEnumerable<int> nullIndexes)
    {
        return CheckBefore(prefix) + $"In {collectionName} has indexes " + string.Join(",", nullIndexes) +
        " with null value";
    }

    /// <summary>
    /// Creates an error message indicating that a collection has an odd number of elements when an even count is expected.
    /// </summary>
    /// <param name="prefix">Prefix for the error message (typically the method name).</param>
    /// <param name="collectionName">The name of the collection being checked.</param>
    /// <returns>Formatted error message.</returns>
    internal static string? NotEvenNumberOfElements(string prefix, string collectionName)
    {
        return CheckBefore(prefix) + collectionName + " have odd elements count";
    }

    /// <summary>
    /// Creates a custom error message with the specified prefix and message.
    /// </summary>
    /// <param name="prefix">Prefix for the error message (typically the method name).</param>
    /// <param name="message">The custom error message.</param>
    /// <returns>Formatted error message.</returns>
    internal static string? Custom(string prefix, string message)
    {
        return CheckBefore(prefix) + message;
    }
    #endregion

    /// <summary>
    /// Creates an error message for a not implemented case.
    /// </summary>
    /// <param name="prefix">Prefix for the error message (typically the method name).</param>
    /// <param name="notImplementedValue">The value or type that is not implemented.</param>
    /// <returns>Formatted error message indicating the case is not implemented.</returns>
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