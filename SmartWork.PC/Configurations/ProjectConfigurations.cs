using Microsoft.Extensions.Configuration;
using System.IO;

namespace SmartWork.PC.Configurations
{
    public class ProjectConfigurations
    {
        public static IConfiguration GetConfigurations()
        {
            const string jsonConfigurationFile = "appsettings.json";
            const string projectName = "SmartWork";

            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string configurationRootPath = Path.Combine(projectDirectory, projectName);

            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(configurationRootPath)
              .AddJsonFile(jsonConfigurationFile)
              .Build();

            return configuration;
        }
    }
}
