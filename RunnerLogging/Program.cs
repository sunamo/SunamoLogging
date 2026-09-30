namespace RunnerLogging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SunamoLogging.Bootstrap;

/// <summary>
/// Entry point of the RunnerLogging console application, which logs through SunamoLogging.
/// </summary>
internal class Program
{
    const string appName = "RunnerLogging";

    static void Main(string[] args)
    {
        var context = LoggingBootstrap.InitConsoleApp(appName, configureServices: services =>
        {
            services.AddScoped<LoggerInner>();
            services.AddScoped<LoggerOuter>();
        });

        var loggerOuter = context.Provider.GetService<LoggerOuter>();
        loggerOuter?.Log();

        context.Logger.LogCritical("END OF WORLD!");

        Console.WriteLine("Finished");
        Console.ReadLine();
    }
}
