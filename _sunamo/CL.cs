namespace SunamoLogging._sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class CL
{
internal static void WriteError(string error)
{
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine(error);
Console.ResetColor();
}
}