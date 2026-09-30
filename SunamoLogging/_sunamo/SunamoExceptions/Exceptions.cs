namespace SunamoLogging._sunamo.SunamoExceptions;

/// <summary>
/// Builds exception and log messages.
/// </summary>
internal static class Exceptions
{
    /// <summary>
    /// Returns the message about null elements of a collection.
    /// </summary>
    internal static string AnyElementIsNullOrEmpty(string prefix, string collectionName, IEnumerable<int> nullIndexes)
    {
        return CheckBefore(prefix) + $"In {collectionName} has indexes " + string.Join(",", nullIndexes) + " with null value";
    }

    /// <summary>
    /// Returns the message about a collection with an odd number of elements.
    /// </summary>
    internal static string NotEvenNumberOfElements(string prefix, string collectionName)
    {
        return CheckBefore(prefix) + collectionName + " have odd elements count";
    }

    /// <summary>
    /// Returns the message of the exception and its inner exceptions, one per line.
    /// </summary>
    internal static string TextOfExceptions(Exception exception)
    {
        var text = new StringBuilder("Exception:");
        text.AppendLine(exception.Message);
        while (exception.InnerException != null)
        {
            exception = exception.InnerException;
            text.AppendLine(exception.Message);
        }
        return text.ToString();
    }

    /// <summary>
    /// Returns the message of the not implemented case.
    /// </summary>
    internal static string NotImplementedCase(string prefix, object notImplementedValue)
    {
        var forClause = notImplementedValue == null ? string.Empty : " for " + (notImplementedValue is Type type ? type.FullName : notImplementedValue.ToString());
        return CheckBefore(prefix) + "Not implemented case" + forClause + " . internal program error. Please contact developer.";
    }

    private static string CheckBefore(string prefix)
    {
        return string.IsNullOrWhiteSpace(prefix) ? string.Empty : prefix + ": ";
    }
}
