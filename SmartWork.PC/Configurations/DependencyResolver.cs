using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartWork.BLL.Services;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Models;
using SmartWork.Data.AppContext;
using SmartWork.Data.Repositories;

namespace SmartWork.PC.Configurations
{
    public class DependencyResolver
    {
        public IConfiguration Configuration { get; }

        public DependencyResolver(IServiceCollection services)
        {
            Configuration = ProjectConfigurations.GetConfigurations();
            ConfigureServices(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            // Register DbContext class
            services.AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            // Register Repositories
            services.AddTransient<IUserRepository<User>, EFCoreUserRepository<User>>();
            services.AddTransient<IRepository<Company>, EFCoreRepository<Company>>();
            services.AddTransient<IRepository<Office>, EFCoreRepository<Office>>();
            services.AddTransient<IRepository<Room>, EFCoreRepository<Room>>();
            services.AddTransient<IRepository<Equipment>, EFCoreRepository<Equipment>>();
            services.AddTransient<IRepository<MaterialEquipment>, EFCoreRepository<MaterialEquipment>>();
            services.AddTransient<IRepository<TechnicalEquipment>, EFCoreRepository<TechnicalEquipment>>();
            services.AddTransient<IRepository<Subscribe>, EFCoreRepository<Subscribe>>();
            services.AddTransient<IRepository<SubscribeDetail>, EFCoreRepository<SubscribeDetail>>();
            services.AddTransient<IRepository<Statistic>, EFCoreRepository<Statistic>>();
            services.AddTransient<IRepository<RoomStatistic>, EFCoreRepository<RoomStatistic>>();
            services.AddTransient<IRepository<VisitStatistic>, EFCoreRepository<VisitStatistic>>();

            // Register Services
            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<IRoleService, RoleService>();
            //services.AddTransient<IMaterialService<Material>, MaterialService<Material>>();
            //services.AddTransient<IMaterialService<Article>, MaterialService<Article>>();
            //services.AddTransient<IMaterialService<VideoMaterial>, MaterialService<VideoMaterial>>();
            //services.AddTransient<IMaterialService<ElectronicBook>, MaterialService<ElectronicBook>>();
            //services.AddTransient<IUserCourseService, UserCourseService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<ISubscribeDetailsService, SubscribeDetailsService>();
            services.AddTransient<ISubscribeService, SubscribeService>();
            //services.AddTransient<IUserSkillService, UserSkillService>();
        }
    }
}