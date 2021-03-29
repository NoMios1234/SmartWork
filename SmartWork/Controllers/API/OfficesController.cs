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
    
    [ApiController]
    public class OfficesController : ControllerBase
    {
        ApplicationContext db;
        public OfficesController(ApplicationContext context)
        {
            db = context;
            if (!db.Office.Any())
            {
                db.Office.Add(new Office
                {
                    OfficeName = "DefalutOffice",
                    OfficeAddress = "DefalutAddress",
                    IsFavourite = true
                });
                db.SaveChanges();
            }
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Office>>> Get()
        {
            List<Office> offices = await db.Office.ToListAsync();
            foreach(var office in offices)
            {
                office.Rooms = await db.Room.Where(r => r.OfficeId == office.Id).ToListAsync();
            }
            return await db.Office.ToListAsync();
        }

        // GET api/offices/5
        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Office>> Get(int id)
        {
            Office office = await db.Office.FirstOrDefaultAsync(x => x.Id == id);
            office.Rooms = await db.Room.Where(r => r.OfficeId == office.Id).ToListAsync();
            return new ObjectResult(office);
        }

        // POST api/offices
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<ActionResult<Office>> Post(Office Office)
        {
            if (Office == null)
            {
                return BadRequest();
            }

            db.Office.Add(Office);
            await db.SaveChangesAsync();
            return Ok(Office);
        }

        // PUT api/offices/
        [Route("api/[controller]")]
        [HttpPut]
        public async Task<ActionResult<Office>> Put(Office Office)
        {
            if (Office == null)
            {
                return BadRequest();
            }
            if (!db.Office.Any(x => x.Id == Office.Id))
            {
                return NotFound();
            }

            db.Update(Office);
            await db.SaveChangesAsync();
            return Ok(Office);
        }

        // DELETE api/offices/5
        [Route("api/[controller]")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Office>> Delete(int id)
        {
            Office Office = db.Office.FirstOrDefault(x => x.Id == id);
            if (Office == null)
            {
                return NotFound();
            }
            db.Office.Remove(Office);
            await db.SaveChangesAsync();
            return Ok(Office);
        }

        //сделать добавление комнат к оффису
        //сделать редактирование подписки    

        /// <summary>
        /// проверит работу этой API 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>

        // GET api/offices/addroom
        //[Route("api/[controller]/[action]/")]
        //[HttpPost]
        //public async Task<ActionResult<Office>> AddRoom(Room room)
        //{
        //    if (room != null)
        //    {
        //        db.Room.Add(room);
        //        await db.SaveChangesAsync();
        //        Office Office = await db.Office.FirstOrDefaultAsync(o => o.id == room.OfficeId);
        //        Office.Rooms = await db.Room.Where(r => r.OfficeId == Office.id).ToListAsync();
        //        return Ok(Office);
        //    }
        //    else
        //        return BadRequest();
        //}
        //// GET api/Offices/officesbyid/5
        //[Route("api/[controller]/[action]")]
        //[HttpPost("{id}")]
        //public async Task<ActionResult<Office>> AddRoomById(int id, )
        //{
        //    Room room = await db.Room.FirstOrDefaultAsync(r => r.Id == id);
        //    if (room != null)
        //    {
        //        db.
        //        db.Room.Add(room);
        //        await db.SaveChangesAsync();
        //        Office Office = await db.Office.FirstOrDefaultAsync(o => o.id == room.OfficeId);
        //        Office.Rooms = await db.Room.Where(r => r.OfficeId == Office.id).ToListAsync();
        //        return Ok(Office);
        //    }
        //    else
        //        return BadRequest();
        //}
    }
}
