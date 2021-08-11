using Microsoft.AspNetCore.Identity;
using SmartWork.Core.Entities;
using SmartWork.PC.Resources;
using System.Threading.Tasks;

namespace SmartWork.PC.Configurations
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var resourceManager = RoleInitializerResources.ResourceManager;

            // Default admin account
            string adminEmail = resourceManager.GetString("adminEmail");
            string adminPassword = resourceManager.GetString("adminPassword");

            // Roles
            string adminRole = resourceManager.GetString("adminRole");
            string employeeRole = resourceManager.GetString("employeeRole");
            string userRole = resourceManager.GetString("userRole");

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(employeeRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(employeeRole));
            }        
            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRole);
                }
            }
        }
    }
}