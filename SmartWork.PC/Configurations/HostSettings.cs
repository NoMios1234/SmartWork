using SmartWork.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using SmartWork.PC.Resources;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartWork.PC.Configurations
{
    public class HostSettings<T>  where T : class
    {
        public static async Task SetBuildSettings(string[] args, bool isRoleInitializer)
        {
            var resourceManager = HostSettingsResources.ResourceManager;
            var appSettings = resourceManager.GetString("appSettings");
            var projectName = resourceManager.GetString("projectName");
            var logFilePath = resourceManager.GetString("logFilePath");
            var fileOutputTemplate = resourceManager.GetString("fileOutputTemplate");

            var projectDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, projectName);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)
                .AddJsonFile(appSettings)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(logFilePath, restrictedToMinimumLevel: LogEventLevel.Error,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: fileOutputTemplate)
                .CreateLogger();
            try
            {
                Log.Information(resourceManager.GetString("logInformation"));
                var hostBuilder = CreateHostBuilder(args);
                var host = hostBuilder.Build();
                if (isRoleInitializer)
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        try
                        {
                            var userManager = services.GetRequiredService<UserManager<User>>();
                            var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                            await RoleInitializer.InitializeAsync(userManager, rolesManager);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, resourceManager.GetString("logError"));
                        }
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, resourceManager.GetString("logFatal"));
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<T>();
                });
    }
}
