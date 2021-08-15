using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using System.Reflection;

namespace SmartWork.Data.AppContext
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<TechnicalEquipment> TechnicalEquipment { get; set; }
        public DbSet<MaterialEquipment> MaterialEquipment { get; set; }
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<SubscribeDetail> SubscribeDetail { get; set; }
        public DbSet<Statistic> Statistic { get; set; }
        public DbSet<RoomStatistic> RoomStatistic { get; set; }
        public DbSet<VisitStatistic> VisitStatistic { get; set; }
    }
}
