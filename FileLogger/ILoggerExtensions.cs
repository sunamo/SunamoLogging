using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
 * Chtěl jsem tu udělat abych nemusel furt volat ručně Serialize
 * Furt mám tuto chybu:
 * 'ILogger' has no applicable method named 'LogInformation' but appears to have an extension method by that name. Extension methods cannot be dynamically dispatched. Consider casting the dynamic arguments or calling the extension method without the extension method syntax.
 * 
 * je to jednoduché - LogInformation je ext. metoda z LoggerExtensions class
 */

namespace SunamoLogging.FileLogger;
public static class ILoggerExtensions
{
    private static void LogInformationJson(this ILogger logger, dynamic d)
    {
        //logger.LogInformation(JsonSerializer.Serialize(d));
    }
}
