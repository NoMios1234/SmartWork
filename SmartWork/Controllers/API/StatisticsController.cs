using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        ApplicationContext db;

        public StatisticsController(ApplicationContext context)
        {
            db = context;

            if (!db.Statistic.Any())
            {
                db.Statistic.Add(new Statistic
                {
                    Description = "Main statistic",
                });
                db.SaveChanges();
            }
            if (!db.RoomStatistic.Any())
            {
                db.RoomStatistic.Add(new RoomStatistic
                {
                    RoomId = db.Room.FirstOrDefault().Id,
                    Description = "Room statistic",
                    Data = "115",
                    StatisticId = db.Statistic.FirstOrDefault().Id
                });
                db.SaveChanges();
            }
            if (!db.VisitStatistic.Any())
            {
                db.VisitStatistic.Add(new VisitStatistic
                {
                    UserId = db.Users.FirstOrDefault().Id,
                    Description = "User statistic",
                    Data = db.Users.FirstOrDefault().Email,
                    StatisticId = db.Statistic.FirstOrDefault().Id
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statistic>>> Get()
        {
            return await db.Statistic.ToListAsync();
        }
    }
}
