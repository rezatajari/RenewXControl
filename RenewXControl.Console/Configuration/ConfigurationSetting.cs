using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;

namespace RenewXControl.Console.Configuration
{
    public class ConfigurationSetting
    {
        public static T ReadConfig<T>(string fileName)
        {
            var json = File.ReadAllText(
                // This line will break the app on runtime in any other machines than yours!
                // Please investigate how you can access to the config file with a relative path in .NET
                // So, it will work regardless of the machine that the app is running on.
                path: $@"D:\Repo\RenewXControl\RenewXControl.Console\Configuration\JsonFiles\{fileName}"
            );
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
