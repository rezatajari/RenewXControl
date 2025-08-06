namespace API.Extensions;

public static class Configuration
{
    public static IConfigurationBuilder AddAppConfigurations(this IConfigurationBuilder builder, IHostEnvironment env)
    {
        return builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}