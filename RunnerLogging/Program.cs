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

    static void Main()
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        var runned = await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs { ServiceCollection = Services });

        var l = Provider.GetService<LoggerOuter>();
        l.Log();

        #region Před zavedením CmdBootstrap
        var sc = new ServiceCollection();
        sc.AddLogging(opt => opt.SetMinimumLevel(LogLevel.Warning));
        sc.AddSingleton(provider =>
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddFile("RunnerLogging");
            const string categoryName = "Any";
            return loggerFactory.CreateLogger(categoryName);
        });
        var sp = sc.BuildServiceProvider();
        var logger = sp.GetRequiredService<ILogger>();
        logger.LogCritical("END OF WORLD!");
        #endregion

        CL.Success("Finished");
        CL.ReadLine();
    }
}