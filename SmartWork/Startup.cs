using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartWork.Models;

namespace SmartWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequiredLength = 8;   // минимальная длина
                options.Password.RequireNonAlphanumeric = true;   // требуются ли не алфавитно-цифровые символы
                options.Password.RequireLowercase = true; // требуются ли символы в нижнем регистре
                options.Password.RequireUppercase = true; // требуются ли символы в верхнем регистре
                options.Password.RequireDigit = true; // требуются ли цифры
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddControllersWithViews();
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "SmartWorkClientApp/dist";
            //});          
            //services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();
            //app.UseCors(builder => builder.WithOrigins("https://localhost:4200/").AllowAnyMethod().AllowAnyHeader());
            //app.UseCors();
            //app.UseDefaultFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllers();
            });
            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "SmartWorkClientApp";
            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}