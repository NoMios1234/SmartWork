using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace SmartWork.Data.AppContext
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        const string PROJECT_NAME = "SmartWork.PC";
        const string APP_SETTINGS = "appsettings.json";
        const string CONNECTION_STRING_NAME = "ConnectionString";
        const string MIGRATIONS_HISTORY_TABLE_NAME = "Migrations";

        public ApplicationContext CreateDbContext(string[] args)
        {
            var projectDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, PROJECT_NAME);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)
                .AddJsonFile(APP_SETTINGS)
                .Build();

            string connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString,
                    o =>
                    {
                        o.MigrationsHistoryTable(MIGRATIONS_HISTORY_TABLE_NAME);
                        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    }).EnableSensitiveDataLogging();

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}