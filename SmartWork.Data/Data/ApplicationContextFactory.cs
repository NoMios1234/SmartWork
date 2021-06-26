using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace SmartWork.Data.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            const string projectName = "SmartWork.Core";
            const string appSettings = "appsettings.json";

            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName
                + @"\" + projectName;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)
                .AddJsonFile(appSettings)
                .Build();

            string connectionString = configuration.GetConnectionString("ConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString,
                    o =>
                    {                      
                        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    }).EnableSensitiveDataLogging();

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}