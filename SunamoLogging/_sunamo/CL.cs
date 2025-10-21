// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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