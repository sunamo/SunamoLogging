namespace SunamoLogging._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool Custom(string message, bool shouldThrow = true, string additionalMessage = "")
    {
        string combinedMessage = string.Join(" ", message, additionalMessage);
        string? exceptionMessage = Exceptions.Custom(FullNameOfExecutedCode(), combinedMessage);
        return ThrowIsNotNull(exceptionMessage, shouldThrow);
    }

    internal static bool CustomWithStackTrace(Exception exception)
    {
        return Custom(Exceptions.TextOfExceptions(exception));
    }

    internal static bool IsNullOrEmpty(string argumentName, string argumentValue)
    {
        return ThrowIsNotNull(Exceptions.IsNullOrWhitespace(FullNameOfExecutedCode(), argumentName, argumentValue, true));
    }

    internal static bool NotImplementedCase(object notImplementedValue)
    {
        return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedValue);
    }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    static string FullNameOfExecutedCode(object typeOrObject, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int stackDepth = 2;
            if (isFromThrowEx)
            {
                stackDepth++;
            }

            methodName = Exceptions.CallingMethod(stackDepth);
        }

        string typeFullName;
        if (typeOrObject is Type asType)
        {
            typeFullName = asType.FullName ?? "Type cannot be get via type is Type";
        }
        else if (typeOrObject is MethodBase methodBase)
        {
            typeFullName = methodBase.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase";
            methodName = methodBase.Name;
        }
        else if (typeOrObject is string)
        {
            typeFullName = typeOrObject.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type actualType = typeOrObject.GetType();
            typeFullName = actualType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exceptionMessage, bool shouldThrow = true)
    {
        if (exceptionMessage != null)
        {
            Debugger.Break();
            if (shouldThrow)
            {
                throw new Exception(exceptionMessage);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode
    internal static bool ThrowIsNotNull<TArgument>(Func<string, TArgument, string?> exceptionFactory, TArgument argument)
    {
        string? exceptionMessage = exceptionFactory(FullNameOfExecutedCode(), argument);
        return ThrowIsNotNull(exceptionMessage);
    }

    #endregion
    #endregion
}
