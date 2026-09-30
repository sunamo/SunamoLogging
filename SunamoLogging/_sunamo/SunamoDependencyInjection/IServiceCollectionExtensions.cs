namespace SunamoLogging._sunamo.SunamoDependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

/// <summary>
/// Registers services by naming convention.
/// </summary>
internal static class IServiceCollectionExtensions
{
    private const string Suffix = "Service";

    /// <summary>
    /// Loads Sunamo*.dll from the application folder and registers (scoped) classes ending with Service, or implementing a non-system interface,
    /// from the loaded Sunamo assemblies (exported types only) and from the entry assembly.
    /// </summary>
    internal static void AddServicesEndingWithService(this IServiceCollection services, ILogger logger)
    {
        var directoryPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty);
        if (string.IsNullOrEmpty(directoryPath))
        {
            return;
        }
        foreach (var dllPath in Directory.GetFiles(directoryPath, "Sunamo*.dll", SearchOption.TopDirectoryOnly))
        {
            var fileName = Path.GetFileNameWithoutExtension(dllPath);
            if (fileName == "SunamoInterfaces")
            {
                continue;
            }
            try
            {
                Assembly.Load(fileName);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to load Sunamo assembly: {AssemblyName}", fileName);
            }
        }

        var sunamoAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly.GetName().Name?.StartsWith("Sunamo") == true && assembly.GetName().Name != "SunamoInterfaces");
        foreach (var assembly in sunamoAssemblies)
        {
            try
            {
                AddServicesFromAssembly(services, assembly, true, logger);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to add services from Sunamo assembly: {AssemblyName}", assembly.GetName().Name);
            }
        }

        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            try
            {
                AddServicesFromAssembly(services, entryAssembly, false, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add services from entry assembly: {AssemblyName}", entryAssembly.GetName().Name);
                throw;
            }
        }
    }

    private static void AddServicesFromAssembly(IServiceCollection services, Assembly assembly, bool isOnlyExported, ILogger logger)
    {
        Type[] types = [];
        try
        {
            types = isOnlyExported ? assembly.GetExportedTypes() : assembly.GetTypes();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to get types from assembly: {AssemblyName}. This can happen with deprecated NuGet packages.", assembly.GetName().Name);
        }

        foreach (var type in types.Where(type => type.IsClass && !type.IsAbstract && !type.IsGenericType))
        {
            var interfaces = type.GetInterfaces()
                .Where(i => !i.IsGenericType && i.Namespace != null && !i.Namespace.StartsWith("System") && !i.Namespace.StartsWith("Microsoft"))
                .ToArray();
            var endsWithSuffix = type.Name.EndsWith(Suffix);
            if (!endsWithSuffix && interfaces.Length == 0)
            {
                continue;
            }

            // Exact naming convention first (UserService -> IUser), then any non-system interface.
            Type? interfaceToRegister = null;
            if (endsWithSuffix)
            {
                interfaceToRegister = interfaces.FirstOrDefault(i => i.Name == $"I{type.Name[..^Suffix.Length]}");
            }
            interfaceToRegister ??= interfaces.FirstOrDefault();

            if (interfaceToRegister != null)
            {
                try
                {
                    services.AddScoped(interfaceToRegister, type);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to register service interface: {InterfaceName} -> {TypeName}", interfaceToRegister.FullName, type.FullName);
                }
            }
            else
            {
                // No interface found, register the concrete type (only for classes ending with the suffix).
                try
                {
                    services.AddScoped(type);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to register service type: {TypeName}", type.FullName);
                    throw;
                }
            }
        }
    }
}
