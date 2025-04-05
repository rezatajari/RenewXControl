using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace RenewXControl.Console.Configuration
{
    public  class ConfigurationSetting
    {
        public static T ReadConfig<T>(string fileName)
        {
            var json  =File.ReadAllText(
                path: $@"D:\Repo\RenewXControl\RenewXControl.Console\Configuration\JsonFiles\{fileName}");
                return JsonSerializer.Deserialize<T>(json);
        }
    }
}
