namespace SunamoLogging._sunamo;

internal class CL
{
    internal static void WriteError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
    }
}
