using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationContext db;

        public StatisticsController(ApplicationContext context)
        {
            db = context;

            if (!db.Statistic.Any())
            {
                db.Statistic.Add(new Statistic
                {
                    StatisticName = "Init stat",
                    StatisticDescription = "Main statistic"
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

        // GET api/statistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statistic>>> Get()
        {
            return await db.Statistic.ToListAsync();
        }

        // GET api/statistics
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Statistic>>> Get(int id)
        {
            Statistic Statistic = await db.Statistic.Where(st => st.Id == id).FirstOrDefaultAsync();
            return new ObjectResult(Statistic);
        }

        // DELETE api/statistics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Statistic>> Delete(int id)
        {
            Statistic Statistic = db.Statistic.FirstOrDefault(o => o.Id == id);
            if (Statistic == null)
            {
                return NotFound();
            }
            db.Statistic.Remove(Statistic);
            await db.SaveChangesAsync();
            return Ok(Statistic);
        }
    }
}
