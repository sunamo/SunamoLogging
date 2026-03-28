namespace SunamoLogging._sunamo;

/// <summary>
/// Console logging helper class.
/// </summary>
internal class CL
{
    /// <summary>
    /// Writes an error message to the console in red color.
    /// </summary>
    /// <param name="errorMessage">The error message to write.</param>
    internal static void WriteError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
    }
}