using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartWork.Models 
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
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
