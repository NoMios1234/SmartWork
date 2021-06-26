using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Models;
using SmartWork.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitStatisticsController : ControllerBase
    {
        ApplicationContext db;

        public VisitStatisticsController(ApplicationContext context)
        {
            db = context;
        }
        // GET api/statistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitStatistic>>> Get()
        {
            return await db.VisitStatistic.ToListAsync();
        }

        // GET api/statistics
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VisitStatistic>>> Get(int id)
        {
            VisitStatistic VisitStatistic = await db.VisitStatistic.Where(st => st.Id == id).FirstOrDefaultAsync();
            return new ObjectResult(VisitStatistic);
        }

        // DELETE api/statistics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Statistic>> Delete(int id)
        {
            VisitStatistic VisitStatistic = db.VisitStatistic.FirstOrDefault(o => o.Id == id);
            if (VisitStatistic == null)
            {
                return NotFound();
            }
            db.VisitStatistic.Remove(VisitStatistic);
            await db.SaveChangesAsync();
            return Ok(VisitStatistic);
        }
    }
}
