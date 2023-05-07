using Microsoft.Extensions.Configuration;

namespace Util
{
    public class ConfigurationManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
