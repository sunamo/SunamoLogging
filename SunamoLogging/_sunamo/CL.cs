namespace SunamoLogging._sunamo;

internal class CL
{
internal static void WriteError(string error)
{
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine(error);
Console.ResetColor();
}
}