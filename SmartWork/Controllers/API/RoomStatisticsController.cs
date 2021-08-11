using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Core.Entities;
using SmartWork.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomStatisticsController : ControllerBase
    {
        ApplicationContext db;

        public RoomStatisticsController(ApplicationContext context)
        {
            db = context;
        }
        // GET api/statistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomStatistic>>> Get()
        {
            return await db.RoomStatistic.ToListAsync();
        }

        // GET api/statistics
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RoomStatistic>>> Get(int id)
        {
            RoomStatistic RoomStatistic = await db.RoomStatistic.Where(st => st.Id == id).FirstOrDefaultAsync();
            return new ObjectResult(RoomStatistic);
        }

        // DELETE api/statistics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Statistic>> Delete(int id)
        {
            RoomStatistic RoomStatistic = db.RoomStatistic.FirstOrDefault(o => o.Id == id);
            if (RoomStatistic == null)
            {
                return NotFound();
            }
            db.RoomStatistic.Remove(RoomStatistic);
            await db.SaveChangesAsync();
            return Ok(RoomStatistic);
        }
    }
}
