namespace RunnerLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SunamoCl;
using SunamoCl.SunamoCmd;
using SunamoLogging.FileLogger;

internal class Program
{
    const string appName = "RunnerLogging";

    static ServiceCollection Services { get; set; } = new();
    static ServiceProvider Provider { get; set; }

    static Program()
    {
        CmdBootStrap.AddILogger(Services, true, FileLoggerProvider.DefaultDirectory(appName), "General");

        Services.AddScoped<LoggerInner>();
        Services.AddScoped<LoggerOuter>();

        Provider = Services.BuildServiceProvider();
    }

    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        var runResult = await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs { ServiceCollection = Services });

        var loggerOuter = Provider.GetService<LoggerOuter>();
        loggerOuter?.Log();

        #region Before CmdBootstrap introduction
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
        serviceCollection.AddSingleton(provider =>
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddFile("RunnerLogging");
            const string categoryName = "Any";
            return loggerFactory.CreateLogger(categoryName);
        });
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var logger = serviceProvider.GetRequiredService<ILogger>();
        logger.LogCritical("END OF WORLD!");
        #endregion

        CL.Success("Finished");
        CL.ReadLine();
    }
}
